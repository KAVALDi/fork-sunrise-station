using Content.Server.Administration;
using Content.Server._Sunrise.Ghost.Events;
using Content.Shared.Administration;
using Robust.Shared.Console;

namespace Content.Server._Sunrise.Ghost.Events.Commands;

[AdminCommand(AdminFlags.Fun)]
public sealed class EndPlanetPrisonEventCommand : LocalizedEntityCommands
{
    [Dependency] private readonly PlanetPrisonEventSystem _eventSystem = default!;

    public override string Command => "endplanetprisonevent";

    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length == 0 || !int.TryParse(args[0], out var eventId))
        {
            shell.WriteError("Использование: endplanetprisonevent <eventId>");
            return;
        }

        if (_eventSystem.EndEvent(eventId))
        {
            shell.WriteLine($"Ивент {eventId} завершен");
        }
        else
        {
            shell.WriteError($"Ивент {eventId} не найден");
        }
    }
}

