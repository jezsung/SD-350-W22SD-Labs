Client user = new User("User1", "user@example.com");
Client userWithBadge1 = new Badge1(user, user.Name, user.Email);
Console.WriteLine(userWithBadge1.GetDescription());

abstract class Client
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Description { get; set; } = "No Description";

    protected Client(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public virtual string GetDescription()
    {
        return Description;
    }
}

class User : Client
{
    public User(string name, string email)
        : base(name, email)
    {
        Description = "Base-level User";
    }
}

abstract class BadgeDecorator : Client
{
    public Client Client { get; set; }

    protected BadgeDecorator(Client client, string name, string email) : base(name, email)
    {
        Client = client;
    }

    public abstract string GetBadge();
}

class Badge1 : BadgeDecorator
{
    public Badge1(Client client, string name, string email) : base(client, name, email)
    {
    }

    public override string GetBadge()
    {
        return "Badge1";
    }

    public override string GetDescription()
    {
        return $"{Client.GetDescription()} {GetBadge()}";
    }
}
