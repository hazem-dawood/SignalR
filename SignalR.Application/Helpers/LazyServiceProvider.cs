namespace SignalR.Application.Helpers;

/// <summary>
/// I have two services
/// <code></code>
/// Service A
/// <code></code>
/// Service B
/// <code></code>
/// I injected Service A inside Service B
/// <code></code>
/// and I injected Service B inside Service A
/// <code></code>
/// It wil throw reference cycle exception
/// <code></code>
/// so this is the solution for that problem
/// <code></code>
/// I inject this service with desired service A or B
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="serviceProvider"></param>
public class LazyServiceProvider<T>(IServiceProvider serviceProvider)
    : Lazy<T>(serviceProvider.GetRequiredService<T>) where T : notnull;
