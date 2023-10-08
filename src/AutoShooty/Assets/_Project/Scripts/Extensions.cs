using Messaging;
using QGame;

public static class Locator
{
    public static IMessageHub MessageHub
    {
        get { return ServiceLocator.Get<IMessageHub>(); }
    }

    public static StatModifierDistributor ModifierDistributor
    {
        get { return ServiceLocator.Get<StatModifierDistributor>(); }
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
        InitializeModifierDistributor();
    }

    private static void InitializeModifierDistributor()
    {
        var existing = Locator.ModifierDistributor;
        if (existing == null)
            ServiceLocator.Register<StatModifierDistributor>(new StatModifierDistributor());
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