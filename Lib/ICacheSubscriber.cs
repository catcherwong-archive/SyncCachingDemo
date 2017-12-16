namespace Lib
{
    public interface ICacheSubscriber
    {
        void Subscribe(string channel, NotifyType notifyType);
    }
}
