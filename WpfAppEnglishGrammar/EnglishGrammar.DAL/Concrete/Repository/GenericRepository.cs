using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EnglishGrammar.DAL.Abstraction.Repository;

namespace EnglishGrammar.DAL.Concrete.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly SqlCommandWrapper _sqlWrapper;

        public GenericRepository(string connection)
        {
            _sqlWrapper = new SqlCommandWrapper(connection);
        }

        public SqlCommandWrapper SqlWrapper
        {
            get { return _sqlWrapper; }
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> ExecuteReader(string spName, Func<SqlDataReader, TEntity> callback, SqlParameter[] parameters = null)
        {
            var result = SqlWrapper.ExecuteReader(CommandType.StoredProcedure, spName, parameters, callback);
            return (IEnumerable<TEntity>)result;
        }
    }
}
