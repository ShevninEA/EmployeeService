namespace EmployeeService.Services
{
    public interface IReposytory<T,TId>
    {
        List<T> GetAll();
        T GetById(TId id);
        int Create(T data);
        void Update(T data);
        void Delete(TId id);
    }
}
