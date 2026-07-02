using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.App.Pages.WordLists;

[QueryProperty(nameof(WordListId), "WordListId")]
public partial class AddingWordsPageVm : ObservableValidator
{
    private readonly IWordListStorage _wordListStorage;

    public AddingWordsPageVm(IWordListStorage wordListStorage)
    {
        _wordListStorage = wordListStorage;
    }

    public string WordListId { get; set; } = string.Empty;

    [ObservableProperty]
    [Required]
    private string _word = string.Empty;

    [ObservableProperty]
    [Required]
    private string _translation = string.Empty;

    [ObservableProperty]
    private string _note = string.Empty;

    public ObservableCollection<Word> Words { get; } = [];

    [RelayCommand]
    private async Task AddWordAsync()
    {
        Word word = new Word
        {
            Id = Guid.NewGuid(),
            Value = Word,
            Translation = Translation,
            Note = Note,
        };

        Words.Add(word);

        await _wordListStorage.AddWordAsync(WordListId, word);

        Word = string.Empty;
        Translation = string.Empty;
        Note = string.Empty;
    }
}
