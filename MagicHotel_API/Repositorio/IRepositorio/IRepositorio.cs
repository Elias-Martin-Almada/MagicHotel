﻿using System.Linq.Expressions;

namespace MagicHotel_API.Repositorio.IRepositorio
{
    // Interface Generico
    public interface IRepositorio<T> where T : class
    {
        Task Crear(T entidad);

        Task<List<T>> ObtenerTodos(Expression<Func<T,bool>> ? filtro = null, string? incluirPropiedades=null);

        Task<T> Obtener(Expression<Func<T,bool>> ? filtro = null, bool tracked = true, string? incluirPropiedades = null);

        Task Remover(T entidad);

        Task Gravar();
    }
}