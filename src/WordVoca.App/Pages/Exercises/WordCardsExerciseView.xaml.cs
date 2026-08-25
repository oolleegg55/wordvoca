namespace WordVoca.App.Pages.Exercises;

public partial class WordCardsExerciseView : ContentPage
{
    private readonly WordCardsExerciseVm _wordCardsExerciseVm;

	public WordCardsExerciseView(WordCardsExerciseVm wordCardsExerciseVm)
	{
		InitializeComponent();

        BindingContext = wordCardsExerciseVm;
        _wordCardsExerciseVm = wordCardsExerciseVm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _wordCardsExerciseVm.InitializeAsync();
    }
}
