namespace WordVoca.App.Pages.WordLists;

public partial class AddingWordsPage : ContentPage
{
    public AddingWordsPage(AddingWordsPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
