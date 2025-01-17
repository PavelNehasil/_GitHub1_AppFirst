﻿using System.Collections.ObjectModel;
using System.Data.SQLite;
using AppFirst.Models;

namespace AppFirst.Services
{
    public class LoadSqlDataSqliteService
    {
        private readonly string _connectionString;

        public LoadSqlDataSqliteService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task LoadSqlDataNowAsync(ObservableCollection<User> users)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, UserName, Password, IsAdmin, Email, FirstName, LastName FROM Users ORDER BY Id;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                Password = reader.IsDBNull(2) ? null : reader.GetString(2),
                                IsAdmin = reader.GetBoolean(3),
                                Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                                FirstName = reader.IsDBNull(5) ? null : reader.GetString(5),
                                LastName = reader.IsDBNull(6) ? null : reader.GetString(6)
                            };

                            users.Add(user);
                        }
                    }
                }
                await connection.CloseAsync();
            }
        }

        public async Task InsertUserAsync(User user)
        {
            bool result = false;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = """
                        INSERT INTO Users (UserName, Password, IsAdmin, Email, FirstName, LastName)
                        VALUES (@userName, @password, @isAdmin, @email, @firstName, @lastName);
                        SELECT last_insert_rowid();
                        """;


                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userName", user.UserName);
                        command.Parameters.AddWithValue("@password", user.Password);
                        command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                        command.Parameters.AddWithValue("@email", user.Email);
                        command.Parameters.AddWithValue("@firstName", user.FirstName);
                        command.Parameters.AddWithValue("@lastName", user.LastName);
                        user.Id = Convert.ToInt32(command.ExecuteScalar());
                        //await command.ExecuteNonQueryAsync();
                    }
                    await connection.CloseAsync();
                    result = true;
                }
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            //return result;
        }

        public async Task DeleteUserAsync(User user)
        {
            bool result = false;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string  query = "DELETE FROM Users WHERE Id=@id;";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", user.Id);
                        await command.ExecuteNonQueryAsync();
                    }
                    await connection.CloseAsync();
                    result = true;
                }
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            //return result;
        }

        public async Task SaveUsersAsync(ObservableCollection<User> users)
        {
            bool result = false;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string  query = "DELETE FROM Users;";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    foreach (User user in users)
                    {
                        query = "INSERT INTO Users (UserName, Password, IsAdmin, Email, FirstName, LastName) VALUES (@userName, @password, @isAdmin, @email, @firstName, @lastName);";
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@userName", user.UserName);
                            command.Parameters.AddWithValue("@password", user.Password);
                            command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                            command.Parameters.AddWithValue("@email", user.Email);
                            command.Parameters.AddWithValue("@firstName", user.FirstName);
                            command.Parameters.AddWithValue("@lastName", user.LastName);
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    await connection.CloseAsync();
                    result = true;
                }
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            //return result;
        }

        public async Task UpdateUserAsync(User user)
        {
            bool result = false;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string updateQuery = "UPDATE Users SET UserName=@userName, Password=@password, IsAdmin=@isAdmin, Email=@email, FirstName=@firstName, LastName=@lastName WHERE Id=@id;";
                    using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userName", user.UserName);
                        command.Parameters.AddWithValue("@password", user.Password);
                        command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                        command.Parameters.AddWithValue("@email", user.Email);
                        command.Parameters.AddWithValue("@firstName", user.FirstName);
                        command.Parameters.AddWithValue("@lastName", user.LastName);
                        command.Parameters.AddWithValue("@id", user.Id);
                        await command.ExecuteNonQueryAsync();
                    }
                    await connection.CloseAsync();
                    result = true;
                }
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            //return result;
        }

        public async Task UserCountAsync()
        {
            int count = 0;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT COUNT(*) FROM Users;";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }
                    await connection.CloseAsync();
                }
            }
            catch (Exception e)
            {
                count = 0;
                throw;
            }
            //return count;
        }
    }
}
