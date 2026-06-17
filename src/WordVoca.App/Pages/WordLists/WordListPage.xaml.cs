namespace WordVoca.App.Pages.WordLists;

public partial class WordListPage : ContentPage
{
    public WordListPage(WordListPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((WordListPageVm)BindingContext).LoadWordsCommand.Execute(null);
    }

}

