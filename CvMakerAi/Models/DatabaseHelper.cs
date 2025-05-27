using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class DatabaseHelper : IDisposable
{
    private readonly SqlConnection _connection;

    public DatabaseHelper()
    {
        _connection = new SqlConnection("Server=BURAK\\SQLEXPRESS;Database=ai_cv;TrustServerCertificate=True;Integrated Security=True;");
        _connection.Open();
    }

    // Execute INSERT, UPDATE, DELETE queries
    public int ExecuteNonQuery(string query, params object[] parameters)
    {
        using (var command = new SqlCommand(query, _connection))
        {
            AddParameters(command, parameters);
            return command.ExecuteNonQuery();
        }
    }

    // Fetch a single value (COUNT, MAX, etc.)
    public object? ExecuteScalar(string query, params object[] parameters)
    {
        using (var command = new SqlCommand(query, _connection))
        {
            AddParameters(command, parameters);
            return command.ExecuteScalar();
        }
    }

    // Fetch multiple rows (SELECT queries)
    public List<Dictionary<string, object>> ExecuteReader(string query, params object[] parameters)
    {
        var result = new List<Dictionary<string, object>>();
        using (var command = new SqlCommand(query, _connection))
        {
            AddParameters(command, parameters);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        row[reader.GetName(i)] = reader[i];

                    result.Add(row);
                }
            }
        }
        return result;
    }

    public List<Dictionary<string, object>> ExecuteQuery(string query, params object[] parameters)
    {
        var result = new List<Dictionary<string, object>>();

        using (var command = new SqlCommand(query, _connection))
        {
            // Parametre ekleme
            for (int i = 0; i < parameters.Length; i++)
            {
                command.Parameters.AddWithValue($"@p{i}", parameters[i] ?? DBNull.Value);
            }

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);
                    }
                    result.Add(row);
                }
            }
        }
        return result;
    }

    public T ExecuteScalar<T>(string query, params object[] parameters)
    {
        using (var command = new SqlCommand(query, _connection))
        {
            AddParameters(command, parameters);
            object result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
                return default!;
            return (T)Convert.ChangeType(result, typeof(T));
        }
    }



    // Add parameters dynamically
    private void AddParameters(SqlCommand command, object[] parameters)
    {
        for (int i = 0; i < parameters.Length; i++)
        {
            command.Parameters.AddWithValue($"@p{i}", parameters[i] ?? DBNull.Value);
        }
    }
    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}
