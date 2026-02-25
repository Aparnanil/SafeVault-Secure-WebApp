using BCrypt.Net;

public class AuthService
{
    private readonly UserRepository _repository;

    public AuthService(UserRepository repository)
    {
        _repository = repository;
    }

    public void Register(string username, string email, string password, string role = "user")
    {
        string hash = BCrypt.Net.BCrypt.HashPassword(password);
        _repository.InsertUser(username, email, hash, role);
    }

    public bool Authenticate(string username, string password)
    {
        var user = _repository.GetUserByUsername(username);
        if (user == null)
            return false;

        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }
}
