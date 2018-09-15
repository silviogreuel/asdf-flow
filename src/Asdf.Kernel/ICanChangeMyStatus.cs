namespace Asdf.Kernel
{
    public interface ICanChangeMyStatus<TStatus> : IHaveStatus<TStatus>
    {
        void Change(TStatus status);
    }
}
