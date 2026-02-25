using NUnit.Framework;

[TestFixture]
public class TestSecurity
{
    [Test]
    public void TestSQLInjectionBlocked()
    {
        string maliciousInput = "' OR 1=1 --";
        string sanitized = InputSanitizer.SanitizeUsername(maliciousInput);

        Assert.IsFalse(sanitized.Contains("'"));
        Assert.IsFalse(sanitized.Contains("--"));
    }

    [Test]
    public void TestXSSBlocked()
    {
        string maliciousScript = "<script>alert('XSS')</script>";
        string sanitized = InputSanitizer.SanitizeInput(maliciousScript);

        Assert.IsFalse(sanitized.Contains("<script>"));
    }

    [Test]
    public void TestPasswordHashing()
    {
        string password = "SecurePass123!";
        string hash = BCrypt.Net.BCrypt.HashPassword(password);

        Assert.IsTrue(BCrypt.Net.BCrypt.Verify(password, hash));
    }
}
