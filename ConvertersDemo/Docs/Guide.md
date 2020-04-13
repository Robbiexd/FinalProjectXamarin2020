# Minigalerie
Druhý příklad demonstruje použítí Converterů v Xamarinu. Zároveň událáme několik změn:
- přesuneme View do [samostatné složky](../ConvertersDemo/Views) a změníme vlastnost v App.cs tak, aby ukazovala na přesunutou MainPage přidáním změněného jmenného prostoru
- zobrazované obrázky nahrafeme do Androidí verze projektu do složky [Resources/drawable](../ConvertersDemo.Android/Resources/drawable)

## ViewModel
[ViewModel](../ConvertersDemo/ViewModels/MainPageViewModel.cs) bude v podstatě standardní včetně dvou Commandů a jedné bindovatelné vlastnosti.

## Convertery
[Convertery](https://docs.microsoft.com/cs-cz/xamarin/xamarin-forms/app-fundamentals/data-binding/converters) implementují rozhraní IConverter a mají tedy dvě metody Convert a ConvertBack.
Do View se připojí do Resources.
````
    <ContentPage.Resources>
        <ResourceDictionary>
            <converters:IntToCityNameConverter x:Key="intToName" />
            <converters:IntToImageConverter x:Key="intToPicture" />
        </ResourceDictionary>
    </ContentPage.Resources>
````    
a použijí se
````
Text="{Binding Index, Converter={StaticResource intToName}}" 
````
 # Použité convertery
 - [První converter](../ConvertersDemo/Converters/IntToCityNameConverter.cs) převádí číslo typu Int na název města
 - [Druhý converter](../ConvertersDemo/Converters/IntToImageConverter.cs) převádí číslo typu Int na název souboru s obrázkem  (tedy ne na připojený obrázek jako v UWP)
 
