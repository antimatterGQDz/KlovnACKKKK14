using Robust.Shared.Prototypes;

namespace Content.Shared._KS14.OnCollide.RemoveComp // KS14 - used for hristov for now, if you want your bullet to remove a component from someone here you go
{
    [RegisterComponent]
    public sealed partial class RemoveCompOnCollideComponent : Component
    {
        [DataField("component", required: true)]
        [AlwaysPushInheritance]
        public ComponentRegistry Components { get; private set; } = new();

    }
}
