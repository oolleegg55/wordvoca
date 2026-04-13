using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.App.Pages.WordLists;

[QueryProperty(nameof(WordListIdString), "WordListId")]
public partial class AddingWordsPageVm : ObservableObject
{
    public ObservableCollection<Word> Words { get; }= [];

    [ObservableProperty]
    private string _word = string.Empty;

    [ObservableProperty]
    private string _translation = string.Empty;

    [ObservableProperty]
    private string _note = string.Empty;

    [ObservableProperty]
    private Guid _wordListId;

    public string WordListIdString
    {
        set
        {
            if (Guid.TryParse(value, out var guid))
            {
                WordListId = guid;
            }
        }
    }

    private readonly IWordListStorage _wordListStorage;

    public AddingWordsPageVm(IWordListStorage wordListStorage)
    {
        _wordListStorage = wordListStorage;
    }

    [RelayCommand]
    private async Task AddWordAsync()
    {
        Words.Add(new Word
        {
           Id = Guid.NewGuid(),
           Value = Word,
           Translation = Translation,
           Note = Note,
        });

        Word = string.Empty;
        Translation = string.Empty;
        Note = string.Empty;
    }

    [RelayCommand]
    private async Task AddWordsToListAsync()
    {
        _wordListStorage.AddWords(WordListId, new(Words));
        await Shell.Current.GoToAsync("..");
    }
}
