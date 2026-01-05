using System.Collections.Generic;
using Robust.Shared.Network;
using Robust.Shared.Player;

namespace Content.Server._Sunrise.Ghost.Events;

public sealed partial class PlanetPrisonEventSystem : EntitySystem
{
    private readonly Dictionary<NetUserId, ICommonSession> _pendingSessions = new();
}
