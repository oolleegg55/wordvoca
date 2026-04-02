using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using WordVoca.Core.Models;

namespace WordVoca.App.Pages.WordLists;

[QueryProperty(nameof(WordListName), "WordListName")]
public partial class WordListPageVm : ObservableObject
{
    public ObservableCollection<Word> Words { get; } = [];

    [ObservableProperty]
    private string _wordListName = string.Empty;

    [ObservableProperty]
    private Guid _wordListId = Guid.Empty;

    public WordListPageVm()
    {
        Words.Add(new Word() { Id = Guid.NewGuid(), Value = "Hello", Translation = "Привет" });

        Words.Add(new Word()
        {
            Id = Guid.NewGuid(),
            Value = "Good",
            Translation = "Хороший",
            Note = "Good progress on the initial prototype."
        });
    }
}
