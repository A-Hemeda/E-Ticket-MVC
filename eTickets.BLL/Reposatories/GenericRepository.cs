using eTickets.DAL.Context;
using eTickets.BLL.Interfaces;
using eTickets.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eTickets.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MvcETicketsAppDbContext _context;

        public GenericRepository(MvcETicketsAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int? id)
            => await _context.Set<T>().FindAsync(id);

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

       

        #region Specification 
        //public async Task<T> GetEntityWithSpecificationAsync(ISpecification<T> specification)
        //    => await ApplySpecification(specification).FirstOrDefaultAsync();



        //public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification)
        //    => await ApplySpecification(specification).ToListAsync();

        //private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        //    => SpecificationEvaluator<T>.MakeQuery(_context.Set<T>().AsQueryable(), specification);

        #endregion


    }
}
