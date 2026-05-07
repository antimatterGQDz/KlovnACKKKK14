using Content.Server.GameTicking.Events;

namespace Content.Server._KS14.IoC;

// This could use harmony or something and be injected when registering IoC or something but idgaf

public sealed class SystemCollectionHookSystem : EntitySystem
{
    [Dependency] private readonly IEntitySystemManager _entitySystemManager = default!;

    /// <inheritdoc cref="EntitySystemManager.DependencyCollection"/>
    [Access(Other = AccessPermissions.ReadExecute)]
    public IDependencyCollection DependencyCollection => _entitySystemManager.DependencyCollection;
    public Action<IDependencyCollection>? OnSystemCollectionAvailable = null;

    private bool _initialisedAllSystems = false;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<RoundStartingEvent>(OnRoundStarting);
    }

    private void TryInitialise()
    {
        if (_initialisedAllSystems)
            return;

        OnSystemCollectionAvailable?.Invoke(DependencyCollection);
        _initialisedAllSystems = true;
    }

    private void OnRoundStarting(RoundStartingEvent args)
    {
        TryInitialise();
    }

    public override void Update(float frameTime) // Best i could do without reflection or some other bs
    {
        base.Update(frameTime);
        TryInitialise();
    }

    public void HookAction(Action act) => OnSystemCollectionAvailable += (_) => act();
}
