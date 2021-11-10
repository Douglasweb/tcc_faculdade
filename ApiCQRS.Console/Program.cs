using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace ApiCQRS.ConsoleProject
{
    class Program
    {
        private static string _connectionString;
        private static string _connectionStringMysql;
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();


            _connectionString = _iconfiguration.GetConnectionString("Default");
            _connectionStringMysql = _iconfiguration.GetConnectionString("MysqlConnection");


            try
            {

                var list = new List<dynamic>();

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT [UserId]
                          ,[UserName]
                          ,[UserEmail]
                          ,[UserCreatedAt]
                          ,[UserUpdatedAt]
                          ,[UserStatus]
                          ,[UserPassword]
                          ,[CanBeUpdated]
                      FROM[TCC].[dbo].[User] WHERE CanBeUpdated = 1", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dynamic user = new ExpandoObject();

                        user.UserId = reader.GetGuid(0);
                        user.UserName = reader.GetString(1);
                        user.UserEmail = reader.GetString(2);
                        user.UserCreatedAt = reader.GetDateTime(3);
                        user.UserUpdatedAt = reader.IsDBNull(4) ? DateTime.Now : reader.GetDateTime(4);
                        user.UserStatus = reader.GetBoolean(5) == true ? "1" : "0";
                        user.UserPassword = reader.GetString(6);
                        user.CanBeUpdated = reader.GetBoolean(7) == true ? "1" : "0";

                        list.Add(user);
                    }
                }

                if (list.Count > 0)
                {
                    
                    MySqlConnection connection = new MySqlConnection(_connectionStringMysql);

                    connection.Open();

                    foreach (var item in list)
                    {

                        CultureInfo arUS = new CultureInfo("en-US");
                        DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;

                        string comandosql = $@"REPLACE INTO user (UserId, UserName, UserEmail,UserCreatedAt,UserUpdateAt,UserStatus,UserPassword,CanBeUpdated) 
                                                VALUES( '{ item.UserId }',
                                                        '{ item.UserName }',
                                                        '{ item.UserEmail }',
                                                        '{ item.UserCreatedAt.ToString("yyyy-MM-dd HH:mm:ss") }',
                                                        '{ item.UserUpdatedAt.ToString("yyyy-MM-dd HH:mm:ss") }',
                                                        b'{ item.UserStatus }',
                                                        '{ item.UserPassword }',
                                                        b'{ item.CanBeUpdated }');";
                        using var command = new MySqlCommand(comandosql, connection);

                        command.ExecuteNonQuery();
                    }

                    connection.Dispose();

                }


                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE [TCC].[dbo].[User] SET [CanBeUpdated] = 0 WHERE [CanBeUpdated] = 1", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();                    
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}


