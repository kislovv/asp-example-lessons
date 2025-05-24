namespace ServicesExample.Api.Pipeline;

public class KeyedServiceFactory(IServiceProvider  serviceProvider) : IKeyedServiceFactory
{
    public T GetService<T>(string key)
    {
        var handler = serviceProvider.GetKeyedService<T>(key)
                      ?? throw new InvalidOperationException($"Service with key '{key}' not registered");

        return handler;
    }
}

public interface IKeyedServiceFactory
{
    T GetService<T>(string key);
}