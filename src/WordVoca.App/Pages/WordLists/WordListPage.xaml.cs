namespace WordVoca.App.Pages.WordLists;

public partial class WordListPage : ContentPage
{
    public WordListPage(WordListPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void HandlePageLoaded(object sender, EventArgs e)
    {
        (BindingContext as WordListPageVm)?.InitializeAsync();
    }
}
