namespace Asdf.Kernel
{
    public interface IHaveId<TId>
    {
        TId Id { get; set; }
    }
}
