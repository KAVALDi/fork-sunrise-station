using Content.Server.Administration;
using Content.Server._Sunrise.Ghost.Events;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Server.Player;

namespace Content.Server._Sunrise.Ghost.Events.Commands;

[AdminCommand(AdminFlags.Fun)]
public sealed class StartPlanetPrisonEventCommand : LocalizedEntityCommands
{
    [Dependency] private readonly PlanetPrisonEventSystem _eventSystem = default!;
    [Dependency] private readonly IPlayerManager _playerMgr = default!;

    public override string Command => "startplanetprisonevent";

    // Использование: startplanetprisonevent <eventId> <role1>:<userId1> <role2>:<userId2> ...
    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length < 2 || !int.TryParse(args[0], out var eventId))
        {
            shell.WriteError("Использование: startplanetprisonevent <eventId> <rolePrototype>:<userId> ...");
            return;
        }
        var winnersDict = new Dictionary<string, NetUserId>();
        for (int i = 1; i < args.Length; i++)
        {
            var split = args[i].Split(':');
            if (split.Length != 2 || string.IsNullOrWhiteSpace(split[0]) || string.IsNullOrWhiteSpace(split[1]))
                continue;
            var roleProto = split[0];
            if (!NetUserId.TryParse(split[1], out var userId))
                continue;
            winnersDict[roleProto] = userId;
        }

        var res = _eventSystem.SetFinalWinnersAndStart(eventId, winnersDict);
        if (!res)
            shell.WriteError("Не удалось запустить ивент!");
        else
            shell.WriteLine($"Ивент {eventId} запущен, роли выданы победителям.");
    }
}

