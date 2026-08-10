using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.App.Pages.WordLists;

[QueryProperty(nameof(WordListId), "WordListId")]
public partial class AddingWordsPageVm : ObservableValidator
{
    private readonly IWordListRepository _wordListStorage;

    public AddingWordsPageVm(IWordListRepository wordListStorage)
    {
        _wordListStorage = wordListStorage;
    }

    public string WordListId { get; set; } = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasAnyProperty))]
    private string _word = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasAnyProperty))]
    private string _translation = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasAnyProperty))]
    private string _note = string.Empty;

    public bool HasAnyProperty => !(string.IsNullOrWhiteSpace(Word) && string.IsNullOrEmpty(Translation) && string.IsNullOrEmpty(Note));

    public ObservableCollection<Word> Words { get; } = [];

    [RelayCommand]
    private async Task AddWordAsync()
    {
        WordList? wordList = await _wordListStorage.GetByIdAsync(WordListId);
        if (wordList is null)
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

        if (!wordList.TryAddWord(word))
        {
            return;
        }

        Words.Add(word);

        await _wordListStorage.SaveAsync(wordList);

        Word = string.Empty;
        Translation = string.Empty;
        Note = string.Empty;
    }
}
