namespace WordVoca.App.Pages.Main;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        (BindingContext as MainPageVm)?.LoadWordListsAsync();
    }
}
