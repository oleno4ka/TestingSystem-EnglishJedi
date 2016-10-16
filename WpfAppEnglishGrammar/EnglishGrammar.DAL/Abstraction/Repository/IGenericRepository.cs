using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EnglishGrammar.DAL.Abstraction.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> ExecuteReader(string spName, Func<SqlDataReader, TEntity> callback,
            SqlParameter[] parameters = null);
    }
}
