using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cfe.Mobile.App.Ordenes.Core.Core.Data {
    public abstract class SQLiteDataContext {
        protected SQLiteConnection _db;
        /// <summary>
        /// Obtiene la conexión para acceder a la base de datos de manera sincrona
        /// </summary>
        protected abstract SQLiteConnection db {
            get;
        }
        protected SQLiteAsyncConnection _dbAsync;
        /// <summary>
        /// Obtiene la conexión para acceder a la base de datos de manera asincrona
        /// </summary>
        protected abstract SQLiteAsyncConnection dbAsync {
            get;
        }

        protected string _nombreBD;
        /// <summary>
        /// Obtiene el nombre de la base de datos
        /// </summary>
        protected abstract string NombreBD {
            get;
        }
        /// <summary>
        /// Método para crear tablas de base de datos de manera asincrona
        /// </summary>
        protected abstract void CreateTablesAsync();
        /// <summary>
        /// Método para crear tablas de base de datos de manera sincrona
        /// </summary>
        protected abstract void CreateTables();

        /// <summary>
        /// Objeto para bloquear accesos a una tabla de la base de datos
        /// </summary>
        static object locker = new object();

        /// <summary>
        /// Inserta una entidad dada en la tabla correspondiente de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="entity">Entidad</param>
        /// <returns>Resultado de la operación</returns>
        public async Task<int> InsertAsync<T>(T entity) => await dbAsync.InsertAsync(entity);
        /// <summary>
        /// Inserta una entidad dada en la tabla correspondiente de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="entity">Entidad</param>
        /// <returns>Resultado de la operación</returns>
        public int Insert<T>(T entity) {
            var result = 0;
            lock (locker) {
                result = db.Insert(entity);
            }
            return result;
        }

        /// <summary>
        /// Actualiza una entidad dada en la tabla correspondiente de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="entity">Entidad</param>
        /// <returns>Resultado de la operación</returns>
        public async Task<int> UpdateAsync<T>(T entity) => await dbAsync.UpdateAsync(entity);
        /// <summary>
        /// Actualiza una entidad dada en la tabla correspondiente de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="entity">Entidad</param>
        /// <returns>Resultado de la operación</returns>
        public int Update<T>(T entity) {
            var result = 0;
            lock (locker) {
                result = db.Update(entity);
            }
            return result;
        }

        /// <summary>
        /// Elimina una entidad dada de la tabla correspondiente de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="entity">Entidad</param>
        /// <returns>Resultado de la operación</returns>
        public async Task<int> DeleteAsync<T>(T entity) => await dbAsync.DeleteAsync(entity);
        /// <summary>
        /// Elimina una entidad dada de la tabla correspondiente de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="entity">Entidad</param>
        /// <returns>Resultado de la operación</returns>
        public int Delete<T>(T entity) {
            var result = 0;
            lock (locker) {
                result = db.Delete(entity);
            }
            return result;
        }

        /// <summary>
        /// Inserta o actualiza una entidad dada en la tabla correspondiente de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="entity">Entidad</param>
        /// <returns>Resultado de la operación</returns>
        public async Task<int> InsertOrReplaceAsync<T>(T entity) => await dbAsync.InsertOrReplaceAsync(entity);
        /// <summary>
        /// Inserta o actualiza una entidad dada en la tabla correspondiente de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="entity">Entidad</param>
        /// <returns>Resultado de la operación</returns>
        public int InsertOrReplace<T>(T entity) {
            var result = 0;
            lock (locker) {
                result = db.InsertOrReplace(entity);
            }
            return result;
        }

        /// <summary>
        /// Obtiene todos los elementos de una tabla dada de manera asincrona
        /// </summary>
        /// <typeparam name="TSource">Tipo de resultado</typeparam>
        /// <returns>Lista de resultados especificados</returns>
        public async Task<List<TSource>> GetAllAsync<TSource>() where TSource : new() => await dbAsync.Table<TSource>().ToListAsync();
        /// <summary>
        /// Obtiene todos los elementos de una tabla dada de manera sincrona
        /// </summary>
        /// <typeparam name="TSource">Tipo de resultado</typeparam>
        /// <returns>Lista de resultados especificados</returns>
        public List<TSource> GetAll<TSource>() where TSource : new() => db.Table<TSource>().ToList();

        /// <summary>
        /// Obtiene un elemento a partir del identificador dado de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="id">Identificador del elemento</param>
        /// <returns>Entidad</returns>
        /// <remarks>Para identificador tipo Int</remarks>
        public async Task<T> GetByIdAsync<T>(int id) where T : new() => await dbAsync.FindAsync<T>(id);
        /// <summary>
        /// Obtiene un elemento a partir del identificador dado de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="id">Identificador del elemento</param>
        /// <returns>Entidad</returns>
        /// <remarks>Para identificador tipo Int</remarks>
        public T GetById<T>(int id) where T : new() => db.Find<T>(id);

        /// <summary>
        /// Obtiene un elemento a partir del identificador dado de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="id">Identificador del elemento</param>
        /// <returns>Entidad</returns>
        /// <remarks>Para identificador tipo Guid</remarks>
        public async Task<T> GetByIdAsync<T>(Guid id) where T : new() => await dbAsync.FindAsync<T>(id);
        /// <summary>
        /// Obtiene un elemento a partir del identificador dado de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="id">Identificador del elemento</param>
        /// <returns>Entidad</returns>
        /// <remarks>Para identificador tipo Guid</remarks>
        public T GetById<T>(Guid id) where T : new() => db.Find<T>(id);

        /// <summary>
        /// Obtiene un elemento a partir del identificador dado de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="id">Identificador del elemento</param>
        /// <returns>Entidad</returns>
        /// <remarks>Para identificador tipo String</remarks>
        public async Task<T> GetByIdAsync<T>(string id) where T : new() => await dbAsync.FindAsync<T>(id);
        /// <summary>
        /// Obtiene un elemento a partir del identificador dado de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="id">Identificador del elemento</param>
        /// <returns>Entidad</returns>
        /// <remarks>Para identificador tipo String</remarks>
        public T GetById<T>(string id) where T : new() => db.Find<T>(id);

        /// <summary>
        /// Obtiene un elemento a partir de una expresión lambda de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="predicate">Predicado de la expresión</param>
        /// <returns>Entidad</returns>
        public async Task<T> GetByExpressionAsync<T>(Expression<Func<T, bool>> predicate) where T : new() => await dbAsync.FindAsync<T>(predicate);
        /// <summary>
        /// Obtiene un elemento a partir de una expresión lambda de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="predicate">Predicado de la expresión</param>
        /// <returns>Entidad</returns>
        public T GetByExpression<T>(Expression<Func<T, bool>> predicate) where T : new() => db.Find<T>(predicate);

        /// <summary>
        /// Obtiene una lista de elementos ordenados a partir de las expresiones lambda dadas de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="predicate">Predicado de la expresión</param>
        /// <param name="orderBy">Expresión de ordenamiento</param>
        /// <returns>Lista de entidades</returns>
        public async Task<List<T>> GetByExpressionOrderByAsync<T, TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null) where TValue : new() where T : new() {
            var query = dbAsync.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }
        /// <summary>
        /// Obtiene una lista de elementos ordenados a partir de las expresiones lambda dadas de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="predicate">Predicado de la expresión</param>
        /// <param name="orderBy">Expresión de ordenamiento</param>
        /// <returns>Lista de entidades</returns>
        public List<T> GetByExpressionOrderBy<T, TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null) where TValue : new() where T : new() {
            var query = db.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return query.ToList();
        }

        /// <summary>
        /// Obtiene un query para personalizar el filtrado y ordenado de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <returns>Query</returns>
        public AsyncTableQuery<T> AsQueryableAsync<T>() where T : new() => dbAsync.Table<T>();
        /// <summary>
        /// Obtiene un query para personalizar el filtrado y ordenado de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <returns>Query</returns>
        public TableQuery<T> AsQueryable<T>() where T : new() => db.Table<T>();

        /// <summary>
        /// Ejecuta la consulta dada de manera asincrona
        /// </summary>
        /// <param name="query">Instrucciones de la consulta</param>
        /// <param name="parameters">Lista de parámetros</param>
        /// <returns>Resultado de la operación</returns>
        public async Task<int> ExecuteAsync(string query, params object[] parameters) => await dbAsync.ExecuteAsync(query, parameters);
        /// <summary>
        /// Ejecuta la consulta dada de manera sincrona
        /// </summary>
        /// <param name="query">Instrucciones de la consulta</param>
        /// <param name="parameters">Lista de parámetros</param>
        /// <returns>Resultado de la operación</returns>
        public int Execute(string query, params object[] parameters) => db.Execute(query, parameters);

        /// <summary>
        /// Ejecuta la consulta dada de manera asincrona, permitiendo regresar el error si sucede
        /// </summary>
        /// <param name="query">Instrucciones de la consulta</param>
        /// <param name="functionParam">Función para recuperar el error</param>
        /// <param name="parameters">Lista de parametros</param>
        /// <returns>Resultado de la operación</returns>
        public async Task<int> ExecuteAsync(string query, Action<Exception> functionParam, params object[] parameters) {
            var result = 1;
            try {
                result = await dbAsync.ExecuteAsync(query, parameters).ConfigureAwait(false);
                functionParam?.Invoke(null);
            } catch (Exception ex) {
                result = 0;
                functionParam?.Invoke(ex);
            }
            return result;
        }

        /// <summary>
        /// Ejecuta la consulta dada de manera sincrona, permitiendo regresar el error si sucede
        /// </summary>
        /// <param name="query">Instrucciones de la consulta</param>
        /// <param name="functionParam">Función para recuperar el error</param>
        /// <param name="parameters">Lista de parametros</param>
        /// <returns>Resultado de la operación</returns>
        public int Execute(string query, Action<Exception> functionParam, params object[] parameters) {
            var result = 1;
            try {
                db.BeginTransaction();
                result = db.Execute(query, parameters);
                db.Commit();
                functionParam?.Invoke(null);
            } catch (Exception ex) {
                result = 0;
                db.Rollback();
                functionParam?.Invoke(ex);
            }
            return result;
        }

        /// <summary>
        /// Ejecuta una consulta que regresa una lista de entidades de manera asincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Instrucciones de la consulta</param>
        /// <param name="parameters">Lista de parámetros</param>
        /// <returns>Lista de entidades especificadas</returns>
        public async Task<List<T>> QueryAsync<T>(string query, params object[] parameters) where T : new() => await dbAsync.QueryAsync<T>(query, parameters);
        /// <summary>
        /// Ejecuta una consulta que regresa una lista de entidades de manera sincrona
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="query">Instrucciones de la consulta</param>
        /// <param name="parameters">Lista de parámetros</param>
        /// <returns>Lista de entidades especificadas</returns>
        public List<T> Query<T>(string query, params object[] parameters) where T : new() => db.Query<T>(query, parameters);

        /// <summary>
        /// Ejecuta la consulta para obtener un valor del tipo dado de manera asincrona
        /// </summary>
        /// <param name="query">Instrucciones de la consulta</param>
        /// <param name="parameters">Lista de parámetros</param>
        /// <returns>Resultado de la operación</returns>
        public async Task<T> ExecuteScalarAsync<T>(string query, params object[] parameters) => await dbAsync.ExecuteScalarAsync<T>(query, parameters);
        /// <summary>
        /// Ejecuta la consulta para obtener un valor del tipo dado de manera sincrona
        /// </summary>
        /// <param name="query">Instrucciones de la consulta</param>
        /// <param name="parameters">Lista de parámetros</param>
        /// <returns>Resultado de la operación</returns>
        public T ExecuteScalar<T>(string query, params object[] parameters) => db.ExecuteScalar<T>(query, parameters);

        /// <summary>
        /// Ejecuta la consulta escalar dada de manera asincrona, permitiendo regresar el error si sucede
        /// </summary>
        /// <param name="query">Instrucciones de la consulta</param>
        /// <param name="functionParam">Función para recuperar el error</param>
        /// <param name="parameters">Lista de parametros</param>
        /// <returns>Resultado de la operación</returns>
        public async Task<T> ExecuteScalarAsync<T>(string query, Action<Exception> functionParam, params object[] parameters) {
            T result = default(T);
            try {
                result = await dbAsync.ExecuteScalarAsync<T>(query, parameters).ConfigureAwait(false);
                functionParam?.Invoke(null);
            } catch (Exception ex) {
                functionParam?.Invoke(ex);
            }
            return result;
        }

        /// <summary>
        /// Ejecuta la consulta dada de manera sincrona, permitiendo regresar el error si sucede
        /// </summary>
        /// <param name="query">Instrucciones de la consulta</param>
        /// <param name="functionParam">Función para recuperar el error</param>
        /// <param name="parameters">Lista de parametros</param>
        /// <returns>Resultado de la operación</returns>
        public T Execute<T>(string query, Action<Exception> functionParam, params object[] parameters) {
            T result = default(T);
            try {
                db.BeginTransaction();
                result = db.ExecuteScalar<T>(query, parameters);
                functionParam?.Invoke(null);
                db.Commit();
            } catch (Exception ex) {
                db.Rollback();
                functionParam?.Invoke(ex);
            }
            return result;
        }
    }
}
