using Content.Server.Administration;
using Content.Server._Sunrise.Ghost.Events;
using Content.Shared.Administration;
using Robust.Shared.Console;

namespace Content.Server._Sunrise.Ghost.Events.Commands;

[AdminCommand(AdminFlags.Fun)]
public sealed class SpawnPlanetPrisonEventCommand : LocalizedEntityCommands
{
    [Dependency] private readonly PlanetPrisonEventSystem _eventSystem = default!;

    public override string Command => "spawnplanetprisonevent";

    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        var minParticipants = 1;
        if (args.Length > 0 && int.TryParse(args[0], out var parsed))
        {
            minParticipants = parsed;
        }

        var eventId = _eventSystem.SpawnEvent(minParticipants);
        shell.WriteLine($"Создан ивент планетарной тюрьмы с ID: {eventId}, минимум участников: {minParticipants}");
    }
}

