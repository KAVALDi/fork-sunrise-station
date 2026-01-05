using Content.Server.Administration;
using Content.Server._Sunrise.Ghost.Events;
using Content.Shared.Administration;
using Robust.Shared.Console;

namespace Content.Server._Sunrise.Ghost.Events.Commands;

[AdminCommand(AdminFlags.Fun)]
public sealed class RemovePlanetPrisonEventCommand : LocalizedEntityCommands
{
    [Dependency] private readonly PlanetPrisonEventSystem _eventSystem = default!;

    public override string Command => "removeplanetprisonevent";

    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length == 0 || !int.TryParse(args[0], out var eventId))
        {
            shell.WriteError("Использование: removeplanetprisonevent <eventId>");
            return;
        }

        if (_eventSystem.RemoveEvent(eventId))
        {
            shell.WriteLine($"Ивент {eventId} удален");
        }
        else
        {
            shell.WriteError($"Ивент {eventId} не найден");
        }
    }
}

