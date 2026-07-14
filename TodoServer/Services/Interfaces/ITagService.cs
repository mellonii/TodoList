namespace TodoServer.Services.Interfaces;

internal interface ITagService
{
    void AddTags(int id, string tags);
    void DeleteTags(int id, string tags);
    string FindByTag(string tag);
    string GetTags();
    string GetTagsById(int id);
}