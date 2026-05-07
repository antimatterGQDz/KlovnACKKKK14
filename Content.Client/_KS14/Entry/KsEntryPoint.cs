using Content.Client._KS14.IoC;
using Content.Shared._KS14.IoC;
using Robust.Shared.ContentPack;
using Robust.Shared.Timing;

namespace Content.Client._KS14.Entry;

internal sealed class KsEntryPoint : GameClient
{
    [Dependency] private readonly SystemCollectionHookManager _systemCollectionHookManager = default!;

    public override void PreInit()
    {
        base.PreInit();
        KsClientContentIoC.Register(Dependencies);
    }

    public override void Init()
    {
        base.Init();
        Dependencies.BuildGraph();
        Dependencies.InjectDependencies(this);
    }

    public override void Update(ModUpdateLevel level, FrameEventArgs frameEventArgs)
    {
        base.Update(level, frameEventArgs);

        if (level != ModUpdateLevel.PreEngine)
            return;

        _systemCollectionHookManager.TryInit();
    }
}
