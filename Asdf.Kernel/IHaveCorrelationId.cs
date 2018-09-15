namespace Asdf.Kernel
{
    public interface IHaveCorrelationId<TId>
    {
        TId CorrelationId { get; set; }
    }
}
