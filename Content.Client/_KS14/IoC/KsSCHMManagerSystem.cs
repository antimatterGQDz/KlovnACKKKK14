using Content.Shared._KS14.IoC;

namespace Content.Client._KS14.IoC;

/// <summary>
///     Manages <see cref="SystemCollectionHookManager"/> being initialised
///         on the client.
/// </summary>
public sealed class KsSCHMManagerSystem : EntitySystem
{
    [Dependency] private readonly SystemCollectionHookManager _systemCollectionHookManager = default!;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        _systemCollectionHookManager.TryInit();
    }
}
