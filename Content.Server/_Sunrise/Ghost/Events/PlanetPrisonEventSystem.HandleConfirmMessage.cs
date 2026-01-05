using Robust.Shared.Network;
using Robust.Shared.Player;

namespace Content.Server._Sunrise.Ghost.Events;

public sealed partial class PlanetPrisonEventSystem
{
    private void HandleConfirmMessage(NetUserId userId, ICommonSession session)
    {
        _pendingSessions[userId] = session;
    }
}
