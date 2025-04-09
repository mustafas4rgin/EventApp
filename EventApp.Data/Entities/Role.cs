namespace EventApp.Data.Entities;

public class Role : EntityBase
{
    public string name { get; set; } = null!;
    public ICollection<User> Users { get; set; } = null!;
}