namespace Asdf.Kernel
{
    public interface IHaveStatus<TStatus>
    {
        TStatus Status { get; set; }
    }
}
