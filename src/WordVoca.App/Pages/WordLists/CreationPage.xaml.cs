namespace WordVoca.App.Pages.WordLists;

public partial class CreationPage : ContentPage
{
    private readonly CreationPageVm _creationPageVm;

    public CreationPage(CreationPageVm vm)
    {
        InitializeComponent();

        BindingContext = vm;
        _creationPageVm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _creationPageVm.InitializeAsync();
    }
}
