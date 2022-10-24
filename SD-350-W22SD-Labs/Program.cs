interface User
{
    public string Password { get; set; }
    public List<string> Tags { get; set; }

    public string PasswordHash();
}

class AuthorizedUser : User
{
    public string Password { get; set; }
    public List<string> Tags { get; set; }


    public AuthorizedUser(string password, List<string> tags)
    {
        Password = password;
        Tags = tags;
    }

    public string PasswordHash()
    {
        return Password + "Authorized";
    }
}

class Administrator : User
{
    public string Password { get; set; }
    public List<string> Tags { get; set; }


    public Administrator(string password, List<string> tags)
    {
        Password = password;
        Tags = tags;
    }

    public string PasswordHash()
    {
        return Password + "Administrator";
    }
}

abstract class UserFactory
{
    public abstract User CreateUser(string password, bool twoFactorAuthentication, bool isAdmin);
}


class TwoFactorAuthenticationFactory : UserFactory
{
    public override User CreateUser(string password, bool twoFactorAuthentication, bool isAdmin)
    {
        if (isAdmin)
        {
            return new Administrator(password, new List<string> { "admin" });
        }
        else if (twoFactorAuthentication)
        {
            return new AuthorizedUser(password, new List<string> { "two-factor-authenticated" });
        }
        else
        {
            throw new NotSupportedException("Two factor authentication is not enabled.");
        }
    }
}

class TwoFactorNotRequiredFactory : UserFactory
{
    public override User CreateUser(string password, bool twoFactorAuthentication, bool isAdmin)
    {
        if (isAdmin)
        {
            return new Administrator(password, new List<string> { "admin" });
        }
        else if (twoFactorAuthentication)
        {
            return new AuthorizedUser(password, new List<string> { "two-factor-authenticated" });
        }
        else
        {
            return new AuthorizedUser(password, new List<string>());
        }
    }
}
