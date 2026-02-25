using MySql.Data.MySqlClient;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InsertUser(string username, string email, string passwordHash, string role)
    {
        string query = "INSERT INTO Users (Username, Email, PasswordHash, Role) " +
                       "VALUES (@Username, @Email, @PasswordHash, @Role)";

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@Role", role);
                command.ExecuteNonQuery();
            }
        }
    }

    public User GetUserByUsername(string username)
    {
        string query = "SELECT * FROM Users WHERE Username = @Username";

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                }
            }
        }
        return null;
    }
}

public class User
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
}
