public interface IEventListener
{
    void RaiseEvent();
}

public interface IEventListener<T>
{
    void RaiseEvent(T parameter);
}