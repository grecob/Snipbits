using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Snippet : INotifyPropertyChanged
{
    private string _title;
    private string _code;
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
    public string Code
    {
        get => _code;
        set { _code = value; OnPropertyChanged(); }
    }
    /// <summary>
    /// The code language of the snippet
    /// </summary>
    public string Language
    {
        get => _language;
        set { _language = value; OnPropertyChanged(); }
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
}