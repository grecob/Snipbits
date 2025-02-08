using Caliburn.Micro;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using MahApps.Metro.Controls;
namespace SnippetManager.ViewModels
{
    public class MainViewModel : MetroWindow, INotifyPropertyChanged
    {
        private readonly ISnippetManagerService _snippetService;
        private ObservableCollection<Snippet> _snippets;
        private Snippet _selectedSnippet;

        public ObservableCollection<Snippet> Snippets
        {
            get => _snippets;
            set { _snippets = value; OnPropertyChanged(); }
        }

        public Snippet SelectedSnippet
        {
            get => _selectedSnippet;
            set { _selectedSnippet = value; OnPropertyChanged(); }
        }

        public ICommand NewSnippetCommand { get; }
        public ICommand SaveSnippetCommand { get; }
        public ICommand DeleteSnippetCommand { get; }

        public MainViewModel(ISnippetManagerService snippetService)
        {
            _snippetService = snippetService;
            Snippets = new ObservableCollection<Snippet>(_snippetService.GetAllSnippets());

            NewSnippetCommand = new RelayCommand(CreateNewSnippet);
            SaveSnippetCommand = new RelayCommand(SaveSnippet);
            DeleteSnippetCommand = new RelayCommand(DeleteSnippet);
        }

        private void CreateNewSnippet()
        {
            var newSnippet = new Snippet
            {
                Title = "New Snippet",
                Language = "C#",
                CreatedDate = DateTime.Now
            };
            Snippets.Add(newSnippet);
            SelectedSnippet = newSnippet;
        }

        private void SaveSnippet()
        {
            if (SelectedSnippet != null)
            {
                _snippetService.SaveSnippet(SelectedSnippet);
            }
        }

        private void DeleteSnippet()
        {
            if (SelectedSnippet != null)
            {
                _snippetService.DeleteSnippet(SelectedSnippet);
                Snippets.Remove(SelectedSnippet);
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}