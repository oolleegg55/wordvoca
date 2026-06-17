using WordVoca.App.Pages.WordLists;

namespace WordVoca.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(CreationPage), typeof(CreationPage));
        Routing.RegisterRoute(nameof(WordListPage), typeof(WordListPage));
        Routing.RegisterRoute(nameof(AddingWordsPage), typeof(AddingWordsPage));
    }
}
