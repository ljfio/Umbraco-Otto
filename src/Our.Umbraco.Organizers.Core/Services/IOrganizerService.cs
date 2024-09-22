using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Engines;

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
    /// Checks if the item has children
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool HasChildren(int id);
}