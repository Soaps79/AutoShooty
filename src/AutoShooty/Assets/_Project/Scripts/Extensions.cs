using Messaging;
using QGame;

public static class Locator
{
    public static IMessageHub MessageHub
    {
        get { return ServiceLocator.Get<IMessageHub>(); }
    }
}

public static class ServiceInitializer
{
    /// <summary>
    /// Will initialize any services unknown to the Locator
    /// </summary>
    public static void Initialize()
    {
        InitializeMessaging();
    }

    // manual initialization
    public static void Initialize<T>(object obj) where T : class
    {
        ServiceLocator.Register<T>(obj);
    }

    private static void InitializeMessaging()
    {
        var existing = Locator.MessageHub;
        if (existing == null)
            ServiceLocator.Register<IMessageHub>(new MessageHub());
    }
}