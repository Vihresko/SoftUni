namespace PersonInfo
{
    public interface IPerson : IIdentifiable, IBirthable, IBuyer
    {
        string Name { get;}
        int Age { get;}
    }
}
