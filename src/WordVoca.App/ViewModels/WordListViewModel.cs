using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using WordVoca.Core.Models;

namespace WordVoca.App.ViewModels;

public partial class WordListViewModel : ObservableObject
{
    public ObservableCollection<WordList> WordLists { get; } = new();

    public WordListViewModel()
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
}
