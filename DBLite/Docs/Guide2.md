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
