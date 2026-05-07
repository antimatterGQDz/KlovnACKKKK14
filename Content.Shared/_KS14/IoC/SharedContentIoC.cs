using Content.Server._KS14.IoC;
using Robust.Shared.ContentPack;
using Robust.Shared.Timing;

namespace Content.Shared._KS14.IoC;

public abstract class KsSharedContentIoC
{
    [Dependency] private readonly SystemCollectionHookManager _systemCollectionHookManager = null!;

    [MustCallBase(false)]
    public virtual void Register(IDependencyCollection dependencyCollection)
    {
        dependencyCollection.Register<SystemCollectionHookManager>();
    }

    [MustCallBase(false)]
    public virtual void Update(ModUpdateLevel level, FrameEventArgs frameEventArgs) { }

    [MustCallBase(false)]
    public virtual void PostInit()
    {
        _systemCollectionHookManager.PostInit();
    }

    [MustCallBase(false)]
    public virtual void Dispose(bool disposing, string destinationFile) { }
}
