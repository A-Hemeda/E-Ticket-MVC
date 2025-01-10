using eTickets.DAL.Entities;


namespace eTickets.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
      
        #region Specefication 
        //Task<T> GetEntityWithSpecificationAsync(ISpecification<T> specification);
        //Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification);
        #endregion

        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(T entity);
    }
}
