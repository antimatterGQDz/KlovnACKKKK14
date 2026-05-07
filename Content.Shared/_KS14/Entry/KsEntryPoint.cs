using Content.Shared._KS14.IoC;
using Robust.Shared.ContentPack;

namespace Content.Shared._KS14.Entry;

public sealed class KsEntryPoint : GameShared
{
    public override void PreInit()
    {
        base.PreInit();
        KsSharedContentIoC.Register(Dependencies);
    }

    public override void Init()
    {
        base.Init();
        Dependencies.BuildGraph();
        Dependencies.InjectDependencies(this);
    }

    public override void PostInit()
    {
        base.PostInit();
    }
}
