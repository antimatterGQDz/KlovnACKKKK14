// SPDX-FileCopyrightText: 2026 LaCumbiaDelCoronavirus
//
// SPDX-License-Identifier: MPL-2.0

using Robust.Shared.Map;
using Robust.Shared.Physics.Systems;
using Robust.Shared.Timing;

namespace Content.Shared._KS14.EnsureWeldJointedEntity;

public sealed class EnsureWeldJointedEntitySystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _gameTiming = default!;
    [Dependency] private readonly SharedJointSystem _jointSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<EnsureWeldJointedEntityComponent, MapInitEvent>(OnStartup); // i lied, its not on spawn
        SubscribeLocalEvent<EnsureWeldJointedEntityComponent, EntParentChangedMessage>(OnParentChanged); // almost as good as MapUidChanged
    }

    private void TrySpawnIfNotInNullspace(Entity<EnsureWeldJointedEntityComponent> entity)
    {
        var transformComponent = Transform(entity);
        if (transformComponent.MapID == MapId.Nullspace)
            return;

        var spawnedUid = Spawn(entity.Comp.SpawnedEntityId, transformComponent.Coordinates);

        var joint = _jointSystem.CreateWeldJoint(entity.Owner, spawnedUid, id: $"ensured-entity-joint-{_gameTiming.CurTick.Value + GetNetEntity(entity).Id}");
        joint.CollideConnected = entity.Comp.CanCollideConnected;

        RemComp(entity, entity.Comp);
    }

    private void OnStartup(Entity<EnsureWeldJointedEntityComponent> entity, ref MapInitEvent args) => TrySpawnIfNotInNullspace(entity);
    private void OnParentChanged(Entity<EnsureWeldJointedEntityComponent> entity, ref EntParentChangedMessage args) => TrySpawnIfNotInNullspace(entity);
}
