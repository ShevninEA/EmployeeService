namespace EmployeeService.Services
{
    public interface IReposytory<T,TId>
    {
        IList<T> GetAll();

        T GetById(TId id);

        TId Create(T data);

        bool Update(T data);

        bool Delete(TId id);
    }
}
