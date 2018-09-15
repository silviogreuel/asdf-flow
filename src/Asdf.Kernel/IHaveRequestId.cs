namespace Asdf.Kernel
{
    public interface IHaveRequestId<TId>
    {
        TId RequestId { get; set; }
    }
}
