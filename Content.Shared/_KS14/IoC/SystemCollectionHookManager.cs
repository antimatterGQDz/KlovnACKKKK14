namespace Content.Server._KS14.IoC;

// This could use harmony or something and be injected when registering IoC or something but idgaf

public sealed class SystemCollectionHookManager
{
    [Dependency] private readonly IEntitySystemManager _entitySystemManager = default!;

    /// <inheritdoc cref="EntitySystemManager.DependencyCollection"/>
    [Access(Other = AccessPermissions.ReadExecute)]
    public IDependencyCollection DependencyCollection => _entitySystemManager.DependencyCollection;
    public Action<IDependencyCollection>? OnSystemCollectionAvailable = null;

    public void PostInit()
    {
        OnSystemCollectionAvailable?.Invoke(DependencyCollection);
    }

    public void HookAction(Action act) => OnSystemCollectionAvailable += (_) => act();
}
