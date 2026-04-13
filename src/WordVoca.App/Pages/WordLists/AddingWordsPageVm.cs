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
    public ObservableCollection<Word> Words { get; }= [];

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
        ValidateAllProperties();
        if (HasErrors)
        {
            return;    
        }

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
