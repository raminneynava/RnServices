using IDP.Api.Domain.IRepository.Command.Base;
using IDP.Api.Infra.Data;

using Microsoft.EntityFrameworkCore;

namespace IDP.Api.Infra.Repository.Command
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        protected readonly IdpCommandDbContext _context;

        public CommandRepository(IdpCommandDbContext context)
        {
            _context = context;
        }
        public async Task<T> Insert(T entity)
        {

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<bool> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }




    }

}
