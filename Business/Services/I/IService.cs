namespace Business.Services;


public interface IService
{
    Task<bool> Exists(int id);        
}
