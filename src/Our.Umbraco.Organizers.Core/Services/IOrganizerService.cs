using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Services;

public interface IOrganizerService<TEntity>
    where TEntity : class, IContentBase
{
    /// <summary>
    /// Saves the item using the correct service
    /// </summary>
    /// <param name="entity"></param>
    void Save(TEntity entity);
    
    /// <summary>
    /// Deletes the item using the correct service
    /// </summary>
    /// <param name="entity"></param>
    void Delete(TEntity entity);
    
    /// <summary>
    /// Creates a folder under the parent item
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parentId"></param>
    /// <param name="folderType"></param>
    /// <returns></returns>
    TEntity CreateFolder(string name, int parentId, string folderType);
    
    /// <summary>
    /// Gets all of the folders under the parent item
    /// </summary>
    /// <param name="folderType"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    IEnumerable<TEntity> GetFolders(int id, string folderType);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    TEntity? GetParent(TEntity entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="parentTypes"></param>
    /// <returns></returns>
    TEntity? GetRoot(TEntity entity, IReadOnlyCollection<string> parentTypes)
    {
        var parent = GetParent(entity);

        if (parent is null)
            return null;

        if (parentTypes.Contains(parent.ContentType.Alias))
            return parent;

        return GetRoot(parent, parentTypes);
    }
    
    /// <summary>
    /// Checks if the item has children
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool HasChildren(int id);
}