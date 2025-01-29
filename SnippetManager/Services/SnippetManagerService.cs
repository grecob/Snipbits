using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Xml;

public class SnippetManagerService : ISnippetManagerService
{
    private readonly string _dataFilePath;
    private readonly List<Snippet> _snippets;
    private readonly object _fileLock = new object();

    public SnippetManagerService()
    {
        // Store snippets in AppData folder
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _dataFilePath = Path.Combine(appDataPath, "SnippetManager", "snippets.json");

        Directory.CreateDirectory(Path.GetDirectoryName(_dataFilePath));

        _snippets = LoadSnippets();
    }

    public IEnumerable<Snippet> GetAllSnippets() => _snippets;

    public void SaveSnippet(Snippet snippet)
    {
        if (!_snippets.Contains(snippet))
        {
            _snippets.Add(snippet);
        }
        SaveToFile();
    }

    public void DeleteSnippet(Snippet snippet)
    {
        _snippets.Remove(snippet);
        SaveToFile();
    }

    public IEnumerable<string> GetAvailableLanguages()
    {
        return new List<string>
        {
            "C#", "JavaScript", "Python", "Java",
            "TypeScript", "HTML", "CSS", "SQL",
            "JSON", "XML", "PHP", "Ruby"
        };
    }

    private List<Snippet> LoadSnippets()
    {
        if (!File.Exists(_dataFilePath))
            return new List<Snippet>();

        try
        {
            lock (_fileLock)
            {
                var json = File.ReadAllText(_dataFilePath);
                return JsonConvert.DeserializeObject<List<Snippet>>(json)
                    ?? new List<Snippet>();
            }
        }
        catch
        {
            return new List<Snippet>();
        }
    }

    private void SaveToFile()
    {
        try
        {
            lock (_fileLock)
            {
                var json = JsonConvert.SerializeObject(_snippets, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(_dataFilePath, json);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving snippets: {ex.Message}");
        }
    }
}