using Robust.Shared.Network;
using Robust.Shared.Serialization;
using System.Collections.Generic;

namespace Content.Shared._Sunrise.Events;

[NetSerializable]
public sealed class ConfirmPrisonEventWinnersMessage : EntityEventArgs
{
    public int EventId { get; set; }
    public Dictionary<string, List<NetUserId>> RoleCandidates { get; set; } = new();
}
