using Content.Client._KS14.IoC;
using Content.Shared._KS14.IoC;
using Robust.Client;
using Robust.Shared.ContentPack;

namespace Content.Client._KS14.Entry;

internal sealed class KsEntryPoint : GameClient
{
    [Dependency] private readonly BaseClient _baseClient = default!;
    [Dependency] private readonly SystemCollectionHookManager _systemCollectionHookManager = default!; // inited on postinit on server, and after player joined on client

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

    public override void PostInit()
    {
        base.PostInit();

        _baseClient.PlayerJoinedServer += (_, _) => _systemCollectionHookManager.TryInit();
    }
}
