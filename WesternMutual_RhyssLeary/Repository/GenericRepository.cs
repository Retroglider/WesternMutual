using System.Linq.Expressions;
using System.Xml.Linq;

namespace WesternMutual_RhyssLeary.Repository;

public interface IGenericRepository<T> where T:class // IEntity (optional) allows us to channel to specific common properties
{
    /// <summary>
    /// Creates a new GUID and stores it in the database along with the metadata provided. If a GUID is not specified, the API will generate a random one.
    /// </summary>
    /// <param name="entity"></param>
    void Create(T entity);
    /// <summary>
    /// Returns the metadata associated to the given GUID.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<T> Read(Expression<Func<T, bool>> expression);
    /// <summary>
    /// Updates the metadata associated to the given GUID. The GUID itself cannot be updated using this command.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task Update(int key, T entity);
    /// <summary>
    /// Deletes the GUID and its associated data.
    /// </summary>
    void Delete(T entity);
}
public class GenericRepository<T>: IGenericRepository<T> where T:class
{
    #region Properties & Fields
    protected readonly ApplicationDbContext Context = default!;
    public IServiceProvider Services { get; private set; } = default!;
    #endregion
    #region Events
    public GenericRepository(ApplicationDbContext context)
    {
        Context = context;
    }
    #endregion
    #region Methods
    public void Create(T entity)
    {
        Context.Set<T>().Add(entity);
    }
    public async Task<T> Read(Expression<Func<T, bool>> expression)
    {
        var result = await Task.FromResult(
            Context.Set<T>()
            .Where(expression)
            .FirstOrDefault());

        if (result is null)
            throw new Exception($"Unable to locate entity {typeof(T).FullName}.");
        
        return result;
    }
    public async Task Update(int key,T entity)
    {
        try
        {
            var exist = await Context.FindAsync<T>(key) ?? default!;

            Context
                .Entry<T>(exist)
                .CurrentValues
                .SetValues(entity);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
    }




    #endregion
}
