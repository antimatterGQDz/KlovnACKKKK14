using System.IO;
using Content.Server._KS14.AnnouncementWebhook;
using Content.Server._KS14.Antag;
using Content.Shared._KS14.IoC;

namespace Content.Server._KS14.IoC;

internal sealed class KsServerContentIoC : KsSharedContentIoC
{
    [Dependency] private readonly LastRolledAntagManager _lastRolledAntagManager = default!;
    [Dependency] private readonly AnnouncementWebhookManager _announcementWebhookManager = default!;

    public override void Register(IDependencyCollection dependencyCollection)
    {
        base.Register(dependencyCollection);

        dependencyCollection.Register<LastRolledAntagManager>();
        dependencyCollection.Register<AnnouncementWebhookManager>();
    }

    public override void Dispose(bool disposing, string destinationFile)
    {
        base.Dispose(disposing, destinationFile);
        _announcementWebhookManager.Shutdown(); // KS14

        if (!string.IsNullOrEmpty(destinationFile))
        {
            _lastRolledAntagManager.Shutdown(); // KS14
        }
    }
}
