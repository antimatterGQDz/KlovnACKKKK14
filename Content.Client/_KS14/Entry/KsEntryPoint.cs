using Content.Client._KS14.IoC;
using Content.Shared._KS14.IoC;
using Robust.Shared.ContentPack;

namespace Content.Client._KS14.Entry;

internal sealed class KsEntryPoint : GameClient
{
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
}
