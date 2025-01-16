using System.Collections.ObjectModel;
using System.Data.SQLite;
using AppFirst.Classes;
using AppFirst.Models;

namespace AppFirst.Services
{
    public class LoadSqlDataSqliteService_LoginImage
    {
        private readonly string _connectionString;

        public LoadSqlDataSqliteService_LoginImage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task GetLoginImages(ObservableCollection<LoginImage> loginImages)
        {
            loginImages.Clear();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("""
                    SELECT Id, ImageName, Description, Image FROM LoginImages
                    ORDER BY Id
                    """, connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var loginImage = new LoginImage
                        {
                            Id = reader.GetInt32(0),
                            ImageName = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Image = reader.IsDBNull(3) ? null : (byte[])reader["Image"]
                        };
                        loginImage.ImageSource = ImageBlobConverter.ByteToBitmapAsync(loginImage.Image).Result;

                        loginImages.Add(loginImage);
                    }
                }
                await connection.CloseAsync();
            }
        }

        public async Task DeleteLoginImageAsync(int idLoginImage)
        {
            if (idLoginImage < 1) return;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("""
                    DELETE FROM LoginImages
                    WHERE Id = @id
                    """, connection);

                command.Parameters.AddWithValue("@id", idLoginImage);
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }

        public async Task InsertLoginImageAsync(LoginImage loginImage)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("""
                    INSERT INTO LoginImages (ImageName, Description, Image) VALUES (@imageName, @description, @image);
                    SELECT last_insert_rowid();
                    """, connection);

                command.Parameters.AddWithValue("@imageName", loginImage.ImageName);
                command.Parameters.AddWithValue("@description", loginImage.Description);
                command.Parameters.AddWithValue("@image", loginImage.Image);
                loginImage.Id = Convert.ToInt32(command.ExecuteScalar());
                //await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }
    }
}
