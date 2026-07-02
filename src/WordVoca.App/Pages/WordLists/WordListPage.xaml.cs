namespace WordVoca.App.Pages.WordLists;

public partial class WordListPage : ContentPage
{
    private readonly WordListPageVm _wordListPageVm;

    public WordListPage(WordListPageVm vm)
    {
        InitializeComponent();

        BindingContext = vm;
        _wordListPageVm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _wordListPageVm.InitializeAsync();
    }
}
