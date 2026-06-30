namespace WordVoca.App.Pages.WordLists;

public partial class CreationPage : ContentPage
{
    public CreationPage(CreationPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
