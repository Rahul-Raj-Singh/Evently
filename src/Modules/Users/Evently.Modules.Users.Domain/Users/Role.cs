namespace Evently.Modules.Users.Domain.Users;

public class Role
{
    public static readonly Role Administrator = new(nameof(Administrator));
    public static readonly Role Member = new(nameof(Member));
    public string Name { get; private set; }
    private Role(string name)
    {
        Name = name;
    }
}
