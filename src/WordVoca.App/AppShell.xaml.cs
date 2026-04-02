namespace WordVoca.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("WordListCreation", typeof(Pages.WordLists.CreationPage));
        Routing.RegisterRoute("WordList", typeof(Pages.WordLists.WordListPage));
    }
}
