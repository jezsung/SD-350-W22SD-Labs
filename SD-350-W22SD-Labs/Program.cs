abstract class Client
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int? Age { get; set; }
    public bool AccessDisabled { get; set; }
    public AccessHandler AccessHandler { get; set; } = null!;

    public Client(string name, string email, int? age, bool accessDisabled)
    {
        Name = name;
        Email = email;
        Age = age;
        AccessDisabled = accessDisabled;
    }

    public virtual void HandleAccess()
    {
        AccessHandler.GetAccess(null, AccessDisabled);
    }
}

class User : Client
{
    public int Reputation { get; set; }

    public User(string name, string email, int? age, bool accessDisabled, int reputation)
        : base(name, email, age, accessDisabled)
    {
        Reputation = reputation;
        AccessHandler = new HasReputation();
    }

    public override void HandleAccess()
    {
        AccessHandler.GetAccess(Reputation, AccessDisabled);
    }
}

class Manager : Client
{
    public Manager(string name, string email, int? age, bool accessDisabled)
        : base(name, email, age, accessDisabled)
    {
        AccessHandler = new HasAccessAutomatic();
    }
}

class Admin : Client
{
    public Admin(string name, string email, int? age, bool accessDisabled)
        : base(name, email, age, accessDisabled)
    {
        AccessHandler = new HasAccessAutomatic();
    }
}

interface AccessHandler
{
    bool GetAccess(int? reputation = 0, bool accessDisabled = false);
}

class HasReputation : AccessHandler
{
    public bool GetAccess(int? reputation = 0, bool accessDisabled = false)
    {
        return reputation != null && reputation > 20;
    }
}
class HasAccessAutomatic : AccessHandler
{
    public bool GetAccess(int? reputation = 0, bool accessDisabled = false)
    {
        return !accessDisabled;
    }
}
