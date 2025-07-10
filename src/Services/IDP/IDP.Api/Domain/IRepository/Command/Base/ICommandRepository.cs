namespace IDP.Api.Domain.IRepository.Command.Base
{
    public interface ICommandRepository<T> where T : class
    {
        Task<T> Insert(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
