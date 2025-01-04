using System.Collections.ObjectModel;
using AppFirst.Models;
using Microsoft.Data.SqlClient;

namespace AppFirst.Services
{
    public class LoadSqlDataPageService
    {
        public string connectionString = string.Empty;

        public LoadSqlDataPageService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task LoadSqlDataNowAsync(ObservableCollection<Order> tableOrders)
        {

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string cannot be null or empty.");
            }

            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                try
                {
                    await databaseConnection.OpenAsync();
                    SqlCommand command = new SqlCommand("SELECT * FROM Orders", databaseConnection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    //tableOrders.Clear();

                    while (await reader.ReadAsync())
                    {
                        Order order = new Order()
                        {
                            OrderID = reader["OrderID"] != DBNull.Value ? int.Parse(reader["OrderID"].ToString()) : 0,
                            CustomerID = reader["CustomerID"]?.ToString() ?? string.Empty,
                            EmployeeID = reader["EmployeeID"] != DBNull.Value ? int.Parse(reader["EmployeeID"].ToString()) : 0,
                            OrderDate = reader["OrderDate"] != DBNull.Value ? DateTime.Parse(reader["OrderDate"].ToString()) : DateTime.MinValue,
                            RequiredDate = reader["RequiredDate"] != DBNull.Value ? DateTime.Parse(reader["RequiredDate"].ToString()) : DateTime.MinValue,
                            ShippedDate = reader["ShippedDate"] != DBNull.Value ? DateTime.Parse(reader["ShippedDate"].ToString()) : DateTime.MinValue,
                            ShipVia = reader["ShipVia"] != DBNull.Value ? int.Parse(reader["ShipVia"].ToString()) : 0,
                            Freight = reader["Freight"] != DBNull.Value ? double.Parse(reader["Freight"].ToString()) : 0.0,
                            ShipName = reader["ShipName"]?.ToString() ?? string.Empty,
                            ShipAddress = reader["ShipAddress"]?.ToString() ?? string.Empty,
                            ShipCity = reader["ShipCity"]?.ToString() ?? string.Empty,
                            ShipRegion = reader["ShipRegion"]?.ToString() ?? string.Empty,
                            ShipPostalCode = reader["ShipPostalCode"]?.ToString() ?? string.Empty,
                            ShipCountry = reader["ShipCountry"]?.ToString() ?? string.Empty
                        };
                        tableOrders.Add(order);
                    }
                }
                catch (Exception ex)
                {
                    tableOrders.Clear();
                    // Log or handle the exception as needed
                    throw;
                }
            }
        }

        public delegate ObservableCollection<Order> AsyncMethodCaller(ObservableCollection<Order> tableOrders, out int threadId);
    }
}
