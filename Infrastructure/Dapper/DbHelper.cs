using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Dapper
{
    public static class DbHelper
    {
        public static SqlConnection GetConn()
        {
            SqlConnection connection = new SqlConnection(DbConn.ConnectionString);
            if (connection.State == ConnectionState.Closed) connection.Open();
            return connection;

        }
        public static IEnumerable<T> RunQueryWithModel<T>(string sql,Dictionary<string,string> dict =null)
        {
            using (var conn = GetConn())
            {
                object param = null;
                if(dict !=null)
                {
                    param = DynamicParam(dict);
                }
                
                return conn.Query<T>(sql, param);
            }
        }
        public async static Task<IEnumerable<T>> RunQueryWithModelAync<T>(string sql, Dictionary<string, string> dict = null)
        {
            using (var conn = GetConn())
            {
                object param = null;
                if (dict != null)
                {
                    param = DynamicParam(dict);
                }
                return await conn.QueryAsync<T>(sql, param);

            }
        }
        public static IEnumerable<dynamic> RunQueryWithoutModel(string sql, Dictionary<string, string> dict = null)
        {

            using (var conn = GetConn())
            {
                object param = null;
                if (dict != null)
                {
                    param = DynamicParam(dict);
                }
                return conn.Query(sql, param);
            }
        }
        public async static Task<IEnumerable<dynamic>> RunQueryWithoutModelAsync(string sql, Dictionary<string, string> dict = null)
        {
            using (var conn = GetConn())
            {
                object param = null;
                if (dict != null)
                {
                    param = DynamicParam(dict);
                }
                return await conn.QueryAsync(sql, param);
            }
        }
        public static void RunSql(string sql, Dictionary<string, string> dict = null)
        {
            using (var con = GetConn())
            {
                object param = null;
                if (dict != null)
                {
                    param = DynamicParam(dict);
                }
                con.Execute(sql, param);
            }
        }
        public static DynamicParameters DynamicParam(Dictionary<string,string> dict)
        {
            DynamicParameters param = new DynamicParameters();
            foreach (var data in dict)
            { 
                param.Add(data.Key,data.Value); 
            }
            return param;

        }
        
    }
}
