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
- [AboutPage](../Views/AboutPage.xaml) - informace o aplikaci
- [ItemsPage](../Views/ItemsPage.xaml) - seznam prvků
- [ItemsDetailPage](../Views/ItemsDetailPage.xaml) - detail prvku
- [MenuPage](../Views/MenuPage.xaml) - navigace aplikace

## Menu
Na MenuPage je seznam odkazů na další stránky. Samotná navigace však probíhá z MainPage, musíme tedy na ni získat odkaz
     
     MainPage RootPage { get => Application.Current.MainPage as MainPage; }

Samotný seznam odkazů bude uložen v seznamu

List<HomeMenuItem> menuItems;

Zde [HomeMenuItem](../Models/HomeMenuItem.cs) je náš vlastní kontejner pro odkazy. (Stejný postupú používá i šablona Master-Detail)
