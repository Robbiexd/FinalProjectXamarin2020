# Prolinkování stránek, zasílání zpráv
V pokračování třetího příkladu si ukážeme několik dalších rysů mobilních aplikací:
- předávání dat mezi stránkami
- zasílání zpráv mezi komponentami
- přidávání nových dat
- mazání dat

V [minulém návodu](Guide1.md) jsme zprovoznili kostru aplikace s prolinkovanými stránkami a funkčním menu. Fungovalo získávání dat a zobrazování detailních informací.

Mezi drobnými vylepšeními, které je potřeba udělat jsou:
- změnit typ indexu u dat z řetězce na int (vlastní generování indexu je zbytečně komplikované)
- v AppDbContextu doplnit metody, které získávají data o třídách

## Formulář pro vkládání dat
Začneme na stránce [../DBLite/Views/NewItemPage.xaml], kam musí přijít vstupní pole pro naše data - Entry a Picker. Stránka nebude mít vlastní ViewModel, bude získávat data z ListViewModelu.
Co bude potřebovat? Bude nutné ji předat seznam tříd. Ten máme k dispozici v [ListViewModelu](../DBLite/ViewModels/ListViewModel.cs) a také v [ItemsPage](../DBLite/Views/ItemsPage.xaml). Předávat je potřebujeme po stisknutí tlašítka "Add".
````
async void AddItem_Clicked(object sender, EventArgs e)
        {
            _vm.LoadClassesCommand.Execute(null);
            Dictionary<int, string> classroomList = new Dictionary<int, string>();
            foreach(var cr in _vm.Classrooms)
            {
                classroomList.Add(cr.Id,cr.Name);
            }
            await Navigation.PushModalAsync(
                new NavigationPage(
                    new NewItemPage(classroomList)
                )
            );
        }
````
Zde získáme z ViewModelu vm seznam tříd a vytvoříme z něj Dictionary obsahující jen data, která potřebujeme. Pak přesuneme na modálně otevřenou stránku NewItemPage, které tato data předáme. NewItemPage je získá v kostruktoru.
````
public NewItemPage(Dictionary<int, string> classroomsList)
        {           
            InitializeComponent();
            Title = "Add new student";
            _classrooms = classroomsList;
            _classIndexes.Clear();
            Classes.Clear();
            foreach (var cr in classroomsList)
            {
                Classes.Add(cr.Value);
                _classIndexes.Add(cr.Key);
            }
        }
 ````
 Vytvoří z nich dva Listy, jeden Classes s názvy tříd bude nabindovaný přímo do Pickeru. (Picker umí obsahovat jen seznam řetězců, které má obsahovat.) Druhý list classIndexes bude obsahovat odpovídající Id tříd. 
 
 Do ostatních polí formuláře budeme bindovat přímo objekt typu Student. Bindování funguje bez ViewModelu proto, protože samotná ContextPage implementuje interface INotifyChanges. Jen je potřeba říct přes  ``BindingContext="{x:Reference Name=NewItemView}" ``, kde je nutné definovat zde použitý název bindované stránky ContextPage přes ``x:Name="NewItemView"``.
 
Samozřejmě před uložením dat bude potřeba převést SelectedClassroom (což je index prvku vybraného v Pickeru) na skutečné číslo třídy.
````
async void Save_Clicked(object sender, EventArgs e)
        {
            if (
                String.IsNullOrEmpty(Student.Firstname) || 
                String.IsNullOrEmpty(Student.Lastname) || 
                SelectedClassroom == null
               )
            {
                await DisplayAlert("Warning", "Incomplete data", "Ok");
            }
            else
            {
                Student.ClassroomId = _classIndexes[(int)SelectedClassroom];
                MessagingCenter.Send(this, "AddStudent", Student);
                MessagingCenter.Send(this, "UpdateStudents");
                await Navigation.PopModalAsync();
            }           
        }
````
Zároveň ověříme validitu dat. Protože jsme tuto stránku otevírali přes ``await Navigation.PushModalAsync(`` vracet se z ní budeme přes  ``await Navigation.PopModalAsync(); ``, která modální okno zavře. 

## Zasílání zpráv

Jak udělat zasílání dat a událostí mezi dvěma ViewModely nebo stránkami? Xamarin obsahuje mechanismus zasílání zpráv. Ten je vysvětlen zde:

- https://montemagno.com/how-to-use-xamarinforms-messaging-center/
- https://docs.microsoft.com/cs-cz/xamarin/xamarin-forms/app-fundamentals/messaging-center

Ve zkratce:
- na jedné straně odesíláme zprávu identifikovanou řetězcem a odesílatelem (stránka nebo ViewModel), můžeme k tomu přibalit i data:
````
MessagingCenter.Send<MainPage, string>(this, "IdentifikátorZprávy", "John");
````
- na druhé straně se někdo může k odebírání této zprávy přihlásit.
````
MessagingCenter.Subscribe<MainPage, Data> (this, "IdentifikátorZprávy", (sender, data) =>
{
    // zpracování dat
});
````
Takto žádá o NewItemPage o přidání studenta do databáze a poté také o aktualizování seznamu studentů.
````
MessagingCenter.Send(this, "AddStudent", Student);
MessagingCenter.Send(this, "UpdateStudents");
````

Obě zprávy zachytí [ListViewModel](../DBLite/ViewModels/ListViewModel.cs):
````
MessagingCenter.Subscribe<NewItemPage>(this, "UpdateStudents", (sender) =>
{
LoadCommand.Execute(null);
});

MessagingCenter.Subscribe<NewItemPage, Student>(this, "AddStudent", async (sender, student) =>
{
if (!await _db.AddItemAsync(student))
    MessagingCenter.Send(this, "ShowAlert", "There was an error.");
});
````

Z ListViewModelu je zasílána zpráva k zobrazení okna zobrazujícího informaci o chybě.
````
MessagingCenter.Send(this, "ShowAlert", "There was an error.");
````
Tu zachytává MainPage.cs
````
MessagingCenter.Subscribe<ListViewModel, string>(this,"ShowAlert", (sender, msg) => { DisplayAlert("Info", msg, "Ok"); });
````

# Přidávání studentů
