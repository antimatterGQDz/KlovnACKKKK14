// SPDX-FileCopyrightText: 2026 LaCumbiaDelCoronavirus
// SPDX-FileCopyrightText: 2026 github_actions[bot]
//
// SPDX-License-Identifier: MPL-2.0

using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._KS14.EnsureWeldJointedEntity;

[RegisterComponent, NetworkedComponent]
public sealed partial class EnsureWeldJointedEntityComponent : Component
{
    [DataField(required: true)]
    public EntProtoId SpawnedEntityId;

    /// <summary>
    ///     Whether the connected bodies can collide with each other.
    /// </summary>
    [DataField]
    public bool CanCollideConnected = true;
}
