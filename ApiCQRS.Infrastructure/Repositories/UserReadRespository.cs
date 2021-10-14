using ApiCQRS.Domain.Entities;
using ApiCQRS.Domain.Interfaces;
using ApiCQRS.Infrastructure.Context;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ApiCQRS.Infrastructure.Repositories
{
    public class UserReadRespository : IUserReadRepository
    {
        
        protected readonly MySqlConnection connection;
        private readonly MysqlConnectConfiguration _mcc;


        public UserReadRespository(MysqlConnectConfiguration mcc)
        {
            _mcc = mcc;            
           connection = new MySqlConnection(_mcc.MysqlUrl);
        }
       

        public async Task<User> GetById(Guid id)
        {
            //db.User.FindAsync(id);
            await connection.OpenAsync();

            using var command = new MySqlCommand($@"SELECT UserId,
                                                            UserName,
                                                            UserEmail,
                                                            UserCreatedAt,
                                                            UserUpdateAt,
                                                            UserStatus,
                                                            UserPassword,
                                                            CanBeUpdated
                                                        FROM user where UserId = '{id.ToString()}'", connection);
            using var reader = await command.ExecuteReaderAsync();

            User user = new User();

            while (await reader.ReadAsync())
            {
                user.UserId = new Guid(reader.GetString(0));
                user.UserName = reader.GetString(1);
                user.UserEmail = reader.GetString(2);
                user.UserCreatedAt = reader.GetDateTime(3);
                user.UserUpdatedAt = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                user.UserStatus = reader.GetBoolean(5);
                user.UserPassword = reader.GetString(6);
                user.UserStatus = reader.GetBoolean(7);
                // do something with 'value'
            }

            connection.Dispose();

            return user;
        }
        public async Task<IEnumerable<User>> GetAll()
        {

            await connection.OpenAsync();

            using var command = new MySqlCommand($@"SELECT UserId,
                                                            UserName,
                                                            UserEmail,
                                                            UserCreatedAt,
                                                            UserUpdateAt,
                                                            UserStatus,
                                                            UserPassword,
                                                            CanBeUpdated
                                                        FROM user;", connection);
            using var reader = await command.ExecuteReaderAsync();

            User user = new User();
            List<User> LstUser = new List<User>();


            while (await reader.ReadAsync())
            {
                var r = reader.GetString(0);
                user.UserId = new Guid(reader.GetString(0));
                user.UserName = reader.GetString(1);
                user.UserEmail = reader.GetString(2);
                user.UserCreatedAt = reader.GetDateTime(3);
                user.UserUpdatedAt = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                user.UserStatus = reader.GetBoolean(5);
                //user.UserPassword = reader.GetString(6);
                user.CanBeUpdated = reader.GetBoolean(7);
                LstUser.Add(user);
            }

            connection.Dispose();

            return LstUser;

            //return await db.User.ToListAsync();
        }
        public async Task<User> GetByEmail(string email)
        {
            await connection.OpenAsync();

            using var command = new MySqlCommand($@"SELECT UserId,
                                                            UserName,
                                                            UserEmail,
                                                            UserCreatedAt,
                                                            UserUpdateAt,
                                                            UserStatus,
                                                            UserPassword,
                                                            CanBeUpdated
                                                        FROM user where UserEmail = '{email}'", connection);
            using var reader = await command.ExecuteReaderAsync();

            User user = new User();

            while (await reader.ReadAsync())
            {
                user.UserId = new Guid(reader.GetString(0));
                user.UserName = reader.GetString(1);
                user.UserEmail = reader.GetString(2);
                user.UserCreatedAt = reader.GetDateTime(3);
                user.UserUpdatedAt = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                user.UserStatus = reader.GetBoolean(5);
                user.UserPassword = reader.GetString(6);
                user.UserStatus = reader.GetBoolean(7);
                // do something with 'value'
            }

            connection.Dispose();

            return user;

            //return await db.User.AsNoTracking().FirstOrDefaultAsync(c => c.UserEmail == email);
        }

    }
}
