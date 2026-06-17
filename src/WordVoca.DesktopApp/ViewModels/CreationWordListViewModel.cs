using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.DesktopApp.ViewModels;

public partial class CreationWordListViewModel : ViewModelBase
{
    private readonly IWordListStorage _wordListStorage;

    public CreationWordListViewModel(IWordListStorage wordListStorage)
    {
        _wordListStorage = wordListStorage;
    }

    public Langs[] Languages { get; } = Enum.GetValues<Langs>();

    [ObservableProperty]
    private string _wordListTitle = string.Empty;

    [ObservableProperty]
    private Langs _sourceLanguage = Langs.En;

    [ObservableProperty]
    private Langs _targetLanguage = Langs.Ru;

    [ObservableProperty]
    private string _defaultWordListTitle = "Word List #1";

    [RelayCommand]
    private void Cancel()
    {
        Reset();
        OnCloseCallback();
    }

    [RelayCommand]
    private async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(WordListTitle))
        {
            WordListTitle = DefaultWordListTitle;
        }

        WordList wordList = new WordList
        {
            Id = Guid.NewGuid(),
            Name = WordListTitle,

            SourceLang = SourceLanguage,
            TargetLang = TargetLanguage,

            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        await _wordListStorage.Save(wordList);

        Reset();
        OnCloseCallback();
    }

    private void Reset()
    {
        WordListTitle = string.Empty;
        SourceLanguage = Langs.En;
        TargetLanguage = Langs.Ru;
    }

    public async Task ChangeDefaultWordListTitle()
    {
        DefaultWordListTitle = $"Word List #{(await _wordListStorage.GetAll()).Count + 1}";
    }
}
