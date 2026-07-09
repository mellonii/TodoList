namespace TodoServer.Services.Interfaces;

internal interface ITagService
{
    void AddTags();
    void DeleteTags();
    void FindByTag();
}