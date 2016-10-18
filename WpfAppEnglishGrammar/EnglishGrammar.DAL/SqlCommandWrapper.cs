using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EnglishGrammar.DAL
{
     public class SqlCommandWrapper
    {
        private readonly string _connectionString;
        public SqlCommandWrapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public object ExecuteReader<T>(CommandType commandType,
           string commandText, SqlParameter[] parameters, Func<SqlDataReader, T> callback = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(commandText, connection) { CommandType = CommandType.StoredProcedure })
                {
                    if(parameters != null)
                       command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.CommandTimeout = 0;

                    var reader = command.ExecuteReader();
                    object result;

                    using (reader)
                    {
                        var list = new List<T>();
                        while (reader.Read())
                        {
                            if (callback != null)
                            {
                                var item = callback(reader);
                                if (!Equals(item, default(T)))
                                {
                                    list.Add(item);
                                }
                            }
                        }

                        result = list;
                    }

                    return result;
                }
            }
        }

        // Review - Oleg: You have checks 23>if(parameters != null){...} and  36>if (callback != null){...} in ExecuteReader<T>
        //method, so methods ExecuteReaderWithParams<T> and ExecuteReaderWithoutParamsAndCallback, in my opinion, unneeded.
         
        public object ExecuteReaderWithParams<T>(CommandType commandType, string commandText, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(commandText, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.CommandTimeout = 0;

                    var result = command.ExecuteReader();

                    return null;
                }
            }
        }

        public object ExecuteReaderWithoutParamsAndCallback(CommandType commandType,
         string commandText)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(commandText, connection) { CommandType = CommandType.StoredProcedure })
                {
                    connection.Open();
                    command.CommandTimeout = 0;

                    var result = command.ExecuteReader();

                    return null;
                }
            }
        }


    }
}
