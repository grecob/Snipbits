public interface ISnippetManagerService
{
    IEnumerable<Snippet> GetAllSnippets();
    void SaveSnippet(Snippet snippet);
    void DeleteSnippet(Snippet snippet);
    IEnumerable<string> GetAvailableLanguages();
}