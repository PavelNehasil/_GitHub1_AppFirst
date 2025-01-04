using System.Collections.ObjectModel;
using System.Data.SQLite;
using AppFirst.Models;

namespace AppFirst.Services
{
    public class LoadSqlDataSqliteService_LoginUser
    {
        private readonly string _connectionString;

        public LoadSqlDataSqliteService_LoginUser(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task GetLoginUsers(ObservableCollection<LoginUser> loginUsers, int idUser)
        {
            loginUsers.Clear();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("""
                    SELECT a.IdUser, a.IdLoginType, b.Name as LoginType, a.Date FROM LoginUsers as a, LoginTypes as b
                    WHERE a.IdUser = @idUser AND a.IdLoginType = b.Id
                    ORDER BY a.Date DESC
                    """, connection);
                command.Parameters.AddWithValue("@idUser", idUser);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var loginUser = new LoginUser
                        {
                            IdUser = reader.GetInt32(0),
                            IdLoginType = reader.GetInt32(1),
                            LoginType = reader.GetString(2),
                            Date = reader.GetDateTime(3)
                        };
                        loginUsers.Add(loginUser);
                    }
                }
                await connection.CloseAsync();
            }
        }

        public async Task DeleteLoginUserAsync(LoginUser loginUser)
        {
            if (loginUser == null) return;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("""
                    DELETE FROM LoginUsers
                    WHERE IdUser = @idUser AND Date = @date
                    """, connection);

                command.Parameters.AddWithValue("@idUser", loginUser.IdUser);
                command.Parameters.AddWithValue("@date", loginUser.Date);
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }

        public async Task DeleteLoginUserIdAsync(int idUser)
        {
            if (idUser < 1) return;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("""
                    DELETE FROM LoginUsers
                    WHERE IdUser = @idUser
                    """, connection);

                command.Parameters.AddWithValue("@idUser", idUser);
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }

        public async Task InsertLoginUserAsync(int idUser, int idLoginType, DateTime date)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("""
                    INSERT INTO LoginUsers (IdUser, IdLoginType, Date) VALUES (@idUser, @idLoginType, @date)
                    """, connection);
                command.Parameters.AddWithValue("@idUser", idUser);
                command.Parameters.AddWithValue("@idLoginType", idLoginType);
                command.Parameters.AddWithValue("@date", date);
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }
    }
}
