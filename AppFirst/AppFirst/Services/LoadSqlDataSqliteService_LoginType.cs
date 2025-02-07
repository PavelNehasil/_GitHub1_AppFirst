using System.Collections.ObjectModel;
using System.Data.SQLite;
using AppFirst.Models;

namespace AppFirst.Services
{
    public class LoadSqlDataSqliteService_LoginType
    {
        private readonly string _connectionString;

        public LoadSqlDataSqliteService_LoginType(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task GetLoginTypes(ObservableCollection<LoginType> loginTypes)
        {
            loginTypes.Clear();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("""
                    SELECT Id, Name, Description FROM LoginTypes
                    ORDER BY Id
                    """, connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var loginType = new LoginType
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2)
                        };
                        loginTypes.Add(loginType);
                    }
                }
                await connection.CloseAsync();
            }
        }

        public async Task DeleteLoginTypeAsync(int idLoginType)
        {
            if (idLoginType < 1) return;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("""
                    DELETE FROM LoginTypes
                    WHERE Id = @id
                    """, connection);

                command.Parameters.AddWithValue("@id", idLoginType);
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }

        public async Task InsertLoginTypeAsync(int idLoginType, string loginTypeName, string description)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("""
                    INSERT INTO LoginTypes (Id, Name, Description) VALUES (@id, @name, @description)
                    """, connection);
                command.Parameters.AddWithValue("@id", idLoginType);
                command.Parameters.AddWithValue("@name", loginTypeName);
                command.Parameters.AddWithValue("@description", description);
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }
    }
}
