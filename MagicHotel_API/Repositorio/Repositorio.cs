using MagicHotel_API.Datos;
using MagicHotel_API.Modelos.Especificaciones;
using MagicHotel_API.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicHotel_API.Repositorio
{
    // Implemento los metodos de la Interface
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        // Inyecto el DbContext
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Crear(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Gravar();
        }

        public async Task Gravar()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if(incluirPropiedades != null) // "Hotel,OtroModelo" <-- Recibe
            {
                foreach(var incluirProp in incluirPropiedades.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (incluirPropiedades != null) // "Hotel,OtroModelo" <-- Recibe
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }
            return await query.ToListAsync();
        }

        public PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (incluirPropiedades != null) // "Hotel,OtroModelo" <-- Recibe
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }
            return PagedList<T>.ToPagedList(query, parametros.PageNumber, parametros.PageSize);
        }

        public async Task Remover(T entidad)
        {
            dbSet.Remove(entidad);
            await Gravar();
        }
    }
}
