using System;
using System.Text.RegularExpressions;
using System.Net;

public static class InputSanitizer
{
    public static string SanitizeUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is required.");

        // Allow only letters, numbers, underscores
        return Regex.Replace(username, @"[^a-zA-Z0-9_]", "");
    }

    public static string SanitizeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format.");

        return WebUtility.HtmlEncode(email);
    }

    public static string SanitizeInput(string input)
    {
        return WebUtility.HtmlEncode(input);
    }
}
