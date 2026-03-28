using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;

namespace WordVoca.App.Pages.Main;

public partial class MainPageVm : ObservableObject
{
    public ObservableCollection<WordList> WordLists { get; } = [];

    public MainPageVm()
    {
        WordLists.Add(new()
        {
            Id = Guid.NewGuid(),
            Name = "Word List #1",
            SourceLang = Langs.En,
            TargetLang = Langs.Es,
        });

        WordLists.Add(new()
        {
            Id = Guid.NewGuid(),
            Name = "Word List #2",
            SourceLang = Langs.Ru,
            TargetLang = Langs.Es,
        });

        WordLists.Add(new()
        {
            Id = Guid.NewGuid(),
            Name = "Word List #3",
            SourceLang = Langs.Ru,
            TargetLang = Langs.En,
        });
    }

    [RelayCommand]
    private async Task CreateAsync()
    {
        await Shell.Current.GoToAsync("WordListCreation");
    }
}
