# Master-detail stránka, přístup k databázi
Třetí příklad demonstruje dva další rysy mobilních aplikací:
- Stránky typu [master-detail](https://docs.microsoft.com/cs-cz/xamarin/xamarin-forms/app-fundamentals/navigation/master-detail-page) (tedy seznam prvků a podrobný pohled na ně)
- Přístup k lokální databázi

## Navigace v aplikaci
Základem bude šablona Blank, snažit se budeme o podobný výsledek jako má šablona Master-Detail, tedy mít jednu stránku se seznamem prvků, druhou s podrobnostmi o jednotlivých prvcích, třetí pro informaci o aplikace.

Podle App.xaml.cs je startovní stránkou aplikace MainPage. Což je obyčejná stránka s obsahem. Proto bude nutné druh stránky změnit na MasterPage.
````
<?xml version="1.0" encoding="utf-8" ?>
<MasterDetailPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             xmlns:views="clr-namespace:DBLite.Views"
             x:Class="DBLite.Views.MainPage">
    <MasterDetailPage.Master>
        <views:MenuPage />
    </MasterDetailPage.Master>
    <MasterDetailPage.Detail>
        <NavigationPage>
            <NavigationPage.Icon>
                <OnPlatform x:TypeArguments="FileImageSource">
                    <On Platform="iOS" Value="tab_feed.png"/>
                </OnPlatform>
            </NavigationPage.Icon>
            <x:Arguments>
                <views:ItemsPage />
            </x:Arguments>
        </NavigationPage>
    </MasterDetailPage.Detail>
</MasterDetailPage>
````
Zajímavé řádky jsou:
1. 20 - Odkaz na stránku, kde bude menu aplikace a která bude řídit navigaci v rámci aplikace
1. 24 - Jak má vypadat ikonka "Hamburger Menu"
1. 30 - Stránka se seznamem prvků a úvodní stránka naší aplikace

Je samozřejmě potřeba vytvořit další navazující stránky, kde tyto už budou ContentPage.
- [AboutPage](../DBLite/Views/AboutPage.xaml) - informace o aplikaci
- [ItemsPage](../DBLite/Views/ItemsPage.xaml) - seznam prvků
- [ItemsDetailPage](../DBLite/Views/ItemsDetailPage.xaml) - detail prvku
- [MenuPage](../DBLite/Views/MenuPage.xaml) - navigace aplikace
- [NewItemPage](../DBLite/Views/NewItemPage.xaml) - přidávání nového prvku (nebude fungovat)

## Menu
Na MenuPage je seznam odkazů na další stránky. Samotná navigace však probíhá z MainPage, musíme tedy na ni získat odkaz
     
     MainPage RootPage { get => Application.Current.MainPage as MainPage; }

Samotný seznam odkazů bude uložen v seznamu

List<HomeMenuItem> menuItems;

Zde [HomeMenuItem](../DBLite/Models/HomeMenuItem.cs) je náš vlastní kontejner pro odkazy. (Stejný postupú používá i šablona Master-Detail) Id je klíč identifikující položku (int, enum), Title je text, který na položce bude napsaný.

Pole naplníme daty
````
menuItems = new List<HomeMenuItem>
{
   new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
   new HomeMenuItem {Id = MenuItemType.About, Title="About" }
};
````
Naplníme ListView ve stránce a nakaždou jeho položku pověsíme obsluhu kliknutí.
````
ListViewMenu.ItemsSource = menuItems;

ListViewMenu.SelectedItem = menuItems[0];
ListViewMenu.ItemSelected += async (sender, e) =>
{
    if (e.SelectedItem == null)
        return;

    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
    await RootPage.NavigateFromMenu(id);
};
````
Zde metoda ``await RootPage.NavigateFromMenu(id);`` volá odpovídající metodu v code-behind MainPage, která se stará o navigaci v aplikaci.
````
public async Task NavigateFromMenu(int id)
{
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage())); // vytvoření stránky na klíči id
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage) // pokud klíč existoval
            {
                Detail = newPage; // nahraď aktuální stránku za tu nově vytvořenou

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100); // a na Androidu chvilku počkej

                IsPresented = false;
            }
}
````
Navigace probíhá tak, že se podle klíče vytvoří odpovídající stránka a tou je nahrazená Detail část Master-detail stránky.

## Tlačítko pro přidávání prvků
Na stránce ItemsPage by měl být seznam prvků a tlačítko pro přidávání nových. Tlačítko je na liště:
````
    <ContentPage.ToolbarItems>
        <ToolbarItem Text="Add" Clicked="AddItem_Clicked" />
    </ContentPage.ToolbarItems>
````
V code-behind je obsluha události, která stránku NewItemPage otevírá jako modální:
````
await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
````
Pokud chceme NewItemPage zavřít, uděláme to jako obsluhu tlačítka v jejím View:
````
await Navigation.PopModalAsync();
````

## Databáze
Na mobilních telefonech obvykle SQL server nespustíme, proto jediná možnost, jakou lze použít je SQLite. Zároveň použijeme EntityFrameworkCore (Microsoft.EntityFrameworkCore.Sqlite). Přidáme ho přes NuGet.

Obsluhu práce s databází uložíme do třídy se službou: [AddDbContext.cs](../DBLite/Services/AddDbContext.cs). Velký problém bude výběr místa, kam se má soubor s daty uložit a jak tuto informaci dostat do našeho Contextu.

Lokace souboru je dána platformou, bude tedy specifikována ve větvi aplikace pro Android v [MainActivity.cs](../DBLite.Android/MainActivity.cs). Ukládat budeme do složky vedle složky Dokumenty.
````
var dbFolderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "..", "Library", "data");
if (!Directory.Exists(dbFolderPath))
{
    Directory.CreateDirectory(dbFolderPath);
}
LoadApplication(new App(Path.Combine(dbFolderPath,"students.sqlite")));
````
Poslední řádek vytváří samotnou aplikaci a námi určenou cestu ji předává.

V App.xaml.cs vytvoříme staticý Context a předáme mu cestu.
````
public App(string dbPath)
{
    InitializeComponent();
    Db = new AppDbContext(dbPath);
    MainPage = new MainPage();
}
````
V AppDbContextu máme vytvoření spojení s databází, obvyklám způsobem založí soubor s databází a provede do něj migrace a obsluhuje základní metody pro manipulaci s daty.

DBContext připojíme do našeho ViewModelu:
````
_db = App.Db;
````
Samotné načtení dat provede Command:
````
LoadCommand = new Command(
async () => {
      IsBusy = true;
      Students = new ObservableCollection<Student>(await _db.GetItemsAsync());
      IsBusy = false;
      }
);
````
Z ItemsPage.cs tento Command vyvoláme z code-behind:
````
protected override void OnAppearing()
{
    base.OnAppearing();

    if (_vm.Students.Count == 0)
    {
        _vm.LoadCommand.Execute(null);
    }
}
````
