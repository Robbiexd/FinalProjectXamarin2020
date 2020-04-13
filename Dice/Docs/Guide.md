# Kostka pro Xamarin
Pro naši první aplikaci napsanou pro Xamarin si ukážeme jednoduchý generátor náhodných čísel fungující jako kostka s volbou počtu stran.
Vzhledem k tomu, že ekvivalentní program máme v UWP, upozorním jen na rozdíly mezi UWP a Xamarin.

Cílem příkladu je ukázat si implementaci architektury MVVM v Xamarinu a že se od nám známého UWP příliš neliší.

## ViewModel
Samotný kód [ViewModelu](../Dice/ViewModel/MainViewModel.cs) je v podstatě stejný. Dokonce se i odvozuje od stejného rozhraní ``INotifyPropertyChanged`` ve stejném jmenném prostoru System.ComponentModel.
Vzhledem k tomu, kód obsahuje totožný

```public event PropertyChangedEventHandler PropertyChanged;```

a v podstatě stejnou metodu pro informování připojených věcí ve View
```
private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
{
     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
```
Stejná bude také struktura datových atributů

``` private int _number; ```

a na ně navázaných vlastností

```
public int Number { get { return _number + 1; } set { _number = value; NotifyPropertyChanged(); } }
```

## Připojení ViewModelu do View
Připravený ViewModel připojíme do [View](../Dice/MainPage.xaml) podobně (ale ne stejně):
```
    <ContentPage.BindingContext>
        <viewmodel:MainViewModel/>
    </ContentPage.BindingContext>
```   
Samotné bindované vlastnosti se do View propíší pomocí ``Text="{Binding Number}"``. Budou fungovat také režimy Bindování (Mode) a Convertery.

## Commandy
Kde nám Xamarin usnadní práci, jsou [Commandy](https://docs.microsoft.com/cs-cz/xamarin/xamarin-forms/app-fundamentals/data-binding/commanding) implementující rozhraní [ICommand](https://docs.microsoft.com/cs-cz/dotnet/api/system.windows.input.icommand?view=netcore-3.1). Zde jsou totiž hotové, jen jsou v jiném jmenném prostoru.

- [Bezparametrický Command](https://docs.microsoft.com/cs-cz/dotnet/api/xamarin.forms.command?view=xamarin-forms)
- [Parametrický Command](https://docs.microsoft.com/cs-cz/dotnet/api/xamarin.forms.command-1?view=xamarin-forms)

Oba mají metodu Execute a metodu CanExecute informující navázané komponenty, že mají bý zapnuté nebo vypnuté.
O tom, že se má zkontrolovat stav proměnných, na kterých je stav Commandu zkontrolovat informujeme kódem
```
if (SetMax != null)((Command)SetMax).ChangeCanExecute();
```
## Styly
Stejně jako v v UWP funguje také stylování komponent:
```
<ContentPage.Resources>
   <Style TargetType="{x:Type Button}" x:Key="GoButton">
       <Setter Property="BackgroundColor" Value="{x:DynamicResource AccentBgColor}" />
       <Setter Property="TextColor" Value="{x:DynamicResource AccentTextColor}" />
   </Style>
</ContentPage.Resources>
```
Zde se jen pro příklad odkazujeme na barvy aplikace definované v souboru [App.xaml](../Dice/App.xaml)
```
<Application.Resources>
   <ResourceDictionary>
       <Color x:Key="LabelColor">#000000</Color>
       <Color x:Key="BgColor">#FFFFFF</Color>
       <Color x:Key="AccentBgColor">#00ff00</Color>
       <Color x:Key="AccentTextColor">#000000</Color>
   </ResourceDictionary>
</Application.Resources>
```
