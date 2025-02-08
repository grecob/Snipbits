using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Snippet : INotifyPropertyChanged
{
    private string _title;
    private TextDocument _document;
    private string _language;
    private DateTime _createdDate;

    /// <summary>
    /// The title of the snippet
    /// </summary>
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }
    /// <summary>
    /// The code content of the snippet
    /// </summary>
    public TextDocument Document
    {
        get => _document;
        set { _document = value; OnPropertyChanged(); }
    }
    /// <summary>
    /// The code language of the snippet
    /// </summary>
    public string Language
    {
        get => _language;
        set { _language = value; OnPropertyChanged(); OnPropertyChanged(nameof(LanguageHighlighting)); Console.WriteLine(HighlightingManager.Instance.GetDefinitionByExtension(Language)); }
    }

    public string Code
    {
        get => Document.Text;
        set => Document = new TextDocument(value);
    }
    /// <summary>
    /// The date and time the snippet was created
    /// </summary>
    public DateTime CreatedDate
    {
        get => _createdDate;
        set { _createdDate = value; OnPropertyChanged(); }
    }

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public IHighlightingDefinition LanguageHighlighting =>
    HighlightingManager.Instance.GetDefinition(Language.ToString());
}
