using CommunityToolkit.Mvvm.ComponentModel;

using WordVoca.Core.Models;

namespace WordVoca.App.Pages.WordLists;

public partial class CreationPageVm : ObservableObject
{
    [ObservableProperty]
    private string _wordListName = string.Empty;

    [ObservableProperty]
    private Langs _sourceLang;

    [ObservableProperty]
    private Langs _targetLang;
}
