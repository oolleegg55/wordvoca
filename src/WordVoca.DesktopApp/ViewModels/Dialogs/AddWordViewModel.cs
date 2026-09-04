using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Repositories;
using WordVoca.DesktopApp.Services;
using WordVoca.DesktopApp.Views.Dialogs;

namespace WordVoca.DesktopApp.ViewModels.Dialogs;

public partial class AddWordViewModel : DialogViewModel
{
    private readonly IWordListRepository _wordListRepository;
    private readonly IDialogService _dialogService;

    private WordList? _wordList;

    public AddWordViewModel(
        IWordListRepository wordListRepository,
        IDialogService dialogService)
    {
        _wordListRepository = wordListRepository;
        _dialogService = dialogService;
    }

    public async Task InitializeAsync()
    {
        _wordList = await _wordListRepository.GetByIdAsync("Word List #1");
    }

    [ObservableProperty]
    private string _value = string.Empty;

    [ObservableProperty]
    private string _translation = string.Empty;

    [ObservableProperty]
    private string _note = string.Empty;

    public ObservableCollection<Word> Words { get; } = [];

    public bool HasWords => Words.Any();

    [RelayCommand]
    public void Cancel()
    {
        OnCloseCallback();
    }

    [RelayCommand]
    public async Task AddWordAsync()
    {
        if (_wordList is null)
        {
            return;
        }

        try
        {
            Word word = new Word()
            {
                Id = Guid.NewGuid(),
                Value = Value,
                Note = Note,
                Translation = Translation
            };

            if (_wordList.TryAddWord(word))
            {
                await _wordListRepository.SaveAsync(_wordList);
                Words.Add(word);

                OnPropertyChanged(nameof(HasWords));

                ResetValues();
            }
        }
        catch (Exception) { }
    }

    [RelayCommand]
    private async Task DeleteWordAsync(Guid wordId)
    {
        if (_wordList is null)
        {
            return;
        }

        try
        {
            bool result = await _dialogService.ShowModalAsync<ConfirmationViewModel, bool>(this);
            if (result)
            {
                if (_wordList.TryRemoveWord(wordId))
                {
                    await _wordListRepository.SaveAsync(_wordList);

                    Word word = Words.First(x => x.Id == wordId);
                    Words.Remove(word);

                    OnPropertyChanged(nameof(HasWords));
                }
            }
        }
        catch (Exception) { }
    }

    private void ResetValues()
    {
        Value = string.Empty;
        Translation = string.Empty;
        Note = string.Empty;
    }
}
