namespace Content.Shared._KS14.IoC;

public sealed class SystemCollectionHookManager
{
    [Dependency] private readonly IEntitySystemManager _entitySystemManager = default!;

    /// <inheritdoc cref="EntitySystemManager.DependencyCollection"/>
    [Access(Other = AccessPermissions.ReadExecute)]
    public IDependencyCollection DependencyCollection => _entitySystemManager.DependencyCollection;
    private Action<IDependencyCollection>? _onSystemCollectionAvailable = null;

    private bool _initalisedCollection = false;

    public void TryInit()
    {
        if (_initalisedCollection)
            return;

        _entitySystemManager.Initialize(); // Justin Case
        _initalisedCollection = true;
        _onSystemCollectionAvailable?.Invoke(DependencyCollection);
    }

    /// <summary>
    ///     Hooks an action to be called when the full <see cref="IDependencyCollection"/>
    ///         is available, calling it immediately if the collection
    ///         is already available.
    /// </summary>
    public void HookAction(Action act)
    {
        if (_initalisedCollection)
        {
            act();
            return;
        }

        _onSystemCollectionAvailable += (_) => act();
    }

    /// <inheritdoc cref="HookAction(Action)"/>
    public void HookAction(Action<IDependencyCollection> act)
    {
        if (_initalisedCollection)
        {
            act(DependencyCollection);
            return;
        }

        _onSystemCollectionAvailable += act;
    }
}
