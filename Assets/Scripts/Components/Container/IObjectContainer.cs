namespace Components.Container
{
    public interface IObjectsContainer
    {
        T Register<T>(T instance) where T : class;
        T Get<T>() where T : class;
        void Unregister<T>() where T : class;
    }
}