using Microsoft.EntityFrameworkCore;
using WebApiPerson.Context;
using static WebApiPerson.Models.EntitiesModels;

namespace WebApiPerson.Services
{
    public class CrudGenericService<TEntityBase> where TEntityBase : EntityBase
    {
        private readonly AppDbContext _context;

        public CrudGenericService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Insert(TEntityBase entity)
        {
            await _context.Set<TEntityBase>().AddAsync(entity);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<TEntityBase> Select(string id)
        {
            var entity = await _context.Set<TEntityBase>().SingleOrDefaultAsync(p => p.Id == id && p!.Status);
            if (entity == null) return null!;
            return entity;
        }


        public async Task<ICollection<TEntityBase>> SelectAll()
        {
            var entities = await _context.Set<TEntityBase>().ToListAsync();
            if (entities == null) return null!;
            return entities;
        }


        public async Task UpdateEntity(TEntityBase entity)
        {
            _context.Set<TEntityBase>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntity(string id)
        {
            var entity = await _context.Set<TEntityBase>().SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) return;

            _context.Set<TEntityBase>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
