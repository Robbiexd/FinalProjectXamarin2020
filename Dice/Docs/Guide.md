# Kostka pro Xamarin
Pro naši první aplikaci napsanou pro Xamarin si ukážeme jednoduchý generátor náhodných čísel fungující jako kostka s volbou velikosti.
Vzhledem k tomu, že ekvivalentní program máme v UWP, upozorním jen na rozdíly mezi UWP a Xamarin.

Cílem příkladu je ukázat si implementaci architektury MVVM v Xamarinu a že se od nám známého UWP příliš neliší.

## ViewModel
Samotný kód [ViewModelu](../ViewModel/MainViewModel.cs) je v podstatě stejný. Dokonce se i odvozuje od stejného rozhraní ``INotifyPropertyChanged`` ve stejném jmenném prostoru System.ComponentModel.
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
Připravený ViewModel připojíme do [View](../MainPage.xaml) podobně (ale ne stejně):

    <ContentPage.BindingContext>
        <viewmodel:MainViewModel/>
    </ContentPage.BindingContext>
    
Samotné bindované vlastnosti se do View propíší pomocí ``Text="{Binding Number}"``. Budou fungovat také režimy Bindování (Mode) a Convertery.

## Commandy
