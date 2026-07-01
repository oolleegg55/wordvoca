using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.App.Pages.WordLists;

[QueryProperty(nameof(WordListIdString), "WordListId")]
public partial class AddingWordsPageVm : ObservableValidator
{
    public ObservableCollection<Word> Words { get; } = [];

    [ObservableProperty]
    [Required]
    private string _word = string.Empty;

    [ObservableProperty]
    [Required]
    private string _translation = string.Empty;

    [ObservableProperty]
    private string _note = string.Empty;

    [ObservableProperty]
    private Guid _wordListId;

    public string WordError => GetErrors(nameof(Word)).FirstOrDefault()?.ErrorMessage ?? string.Empty;

    private string _wordListIdString;

    public string WordListIdString
    {
        get
        {
            return _wordListIdString;
        }
        set
        {
            _wordListIdString = value;
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
        ValidateAllProperties();
        if (HasErrors)
        {
            return;
        }

        Word word = new Word
        {
            Id = Guid.NewGuid(),
            Value = Word,
            Translation = Translation,
            Note = Note,
        };

        Words.Add(word);

        await _wordListStorage.AddWordAsync(WordListIdString, word);
        Word = string.Empty;
        Translation = string.Empty;
        Note = string.Empty;
    }
}
