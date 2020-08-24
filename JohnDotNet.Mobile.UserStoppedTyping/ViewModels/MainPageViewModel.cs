using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace JohnDotNet.Mobile.UserStoppedTyping.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        #region Properties
        
        private string _performedSearches;
        public string PerformedSearches
        {
            get => _performedSearches;
            set => SetProperty(ref _performedSearches, value);
        }

        public ICommand SearchCommand { get; }

        #endregion Properties

        public MainPageViewModel()
        {
            SearchCommand = new Command<string>(PerformSearch);
        }

        private void PerformSearch(string searchTerms)
        {
            PerformedSearches += $"Performed search for '{searchTerms}'{Environment.NewLine}";
        }
    }
}