using AutoMapper;

using WesternMutual_RhyssLeary.Domain;
using WesternMutual_RhyssLeary.Repository;

using static WesternMutual_RhyssLeary.Models.Entry;

namespace WesternMutual_RhyssLeary.Services;

public interface IGuidService
{
    /// <summary>
    /// Creates a new GUID and stores it in the database along with the metadata provided. If a GUID is not specified, the API will generate a random one.
    /// </summary>
    /// <returns></returns>
    Task<ViewModel> Create(Guid? guid, CreateModel createModel);
    /// <summary>
    /// Returns the metadata associated to the given GUID.
    /// </summary>
    /// <returns></returns>
    Task<ViewModel> Read(Guid guid);
    /// <summary>
    /// Updates the metadata associated to the given GUID. The GUID itself cannot be updated using this command.
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="updateModel"></param>
    /// <returns></returns>
    Task<ViewModel> Update(Guid guid, UpdateModel updateModel);
    /// <summary>
    /// Deletes the GUID and its associated data.
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task Delete(Guid guid);
}
public class GuidService : IGuidService
{
    #region Properties & Fields
    IServiceProvider Services { get; }
    IMapper Mapper { get; }
    
    IUnitOfWork UnitOfWork { get; }
    
    #endregion
    #region Events
    public GuidService(IServiceProvider services)
    {
        Services = services;
        UnitOfWork = Services.GetRequiredService<IUnitOfWork>();
        Mapper = Services.GetRequiredService<IMapper>();
    }
    #endregion
    #region Methods

    public async Task<ViewModel> Create(Guid? guid, CreateModel createModel)
    {

        var expire = createModel.Expire == null
            ? DateTime.Now.AddDays(AppConfig.Defaults.ExpirationDays) // Default value
            : ((long)createModel.Expire).ToDate(); // Specific date 

        //var entry = Mapper.Map<Entry>(createModel);
        Entry entry = new()
        {
            User = createModel.User,
            Expire = expire
        };
        entry.Guid = guid ?? entry.Guid; // If provided, use this GUID, otherwise generate new GUID.
        UnitOfWork.Entries.Create(entry);
        await UnitOfWork.CommitAsync();
        var viewModel = Mapper.Map<ViewModel>(entry);
        return viewModel;
    }

    public async Task<ViewModel> Read(Guid guid)
    {
        var entry = await UnitOfWork.Entries.Read(g => g.Guid == guid);
        var viewModel = Mapper.Map<ViewModel>(entry);
        return viewModel;
    }
    
    public async Task<ViewModel> Update(Guid guid,UpdateModel updateModel)
    {
        var entry = await UnitOfWork.Entries.Read(g => g.Guid == guid);
        if (updateModel.Expire is not null)
        {
            if (long.TryParse(updateModel.Expire, out long expireDate))
            {
                entry.Expire = expireDate.ToDate();
            }
            else
                throw new Exception($"Invalid expiration date provided ({updateModel.Expire}).");
        }
        // Optional update of User
        entry.User = updateModel.User ?? entry.User;
        
        await UnitOfWork.Entries.Update(entry.Id,entry);
        await UnitOfWork.CommitAsync();
        
        var viewModel = Mapper.Map<ViewModel>(entry);
        return viewModel;
    }

    public async Task Delete(Guid guid)
    {
        var entry = await UnitOfWork.Entries.Read(g => g.Guid == guid);
        UnitOfWork.Entries.Delete(entry);
        await UnitOfWork.CommitAsync();
    }
    #endregion
}

