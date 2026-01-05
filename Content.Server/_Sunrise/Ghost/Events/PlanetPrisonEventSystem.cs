using Content.Server.Administration;
using Content.Shared.GameTicking;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Shared.Timing;

namespace Content.Server._Sunrise.Ghost.Events;

// Sunrise-Start: система для управления ивентами планетарной тюрьмы
public sealed class PlanetPrisonEventSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;

    private int _nextEventId = 1;
    private Dictionary<int, PlanetPrisonEvent> _activeEvents = new();

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<RoundRestartCleanupEvent>(OnRoundRestart);
    }

    private void OnRoundRestart(RoundRestartCleanupEvent ev)
    {
        ResetEventIds();
    }

    public int SpawnEvent(int minParticipants = 1)
    {
        var eventId = _nextEventId++;
        var newEvent = new PlanetPrisonEvent
        {
            Id = eventId,
            MinParticipants = minParticipants,
            StartTime = _timing.CurTime
        };
        _activeEvents[eventId] = newEvent;
        return eventId;
    }

    public bool EndEvent(int eventId)
    {
        if (!_activeEvents.TryGetValue(eventId, out var evt))
            return false;

        evt.IsActive = false;
        return true;
    }

    public bool RemoveEvent(int eventId)
    {
        return _activeEvents.Remove(eventId);
    }

    public PlanetPrisonEvent? GetEvent(int eventId)
    {
        _activeEvents.TryGetValue(eventId, out var evt);
        return evt;
    }

    public void ResetEventIds()
    {
        _nextEventId = 1;
        _activeEvents.Clear();
    }
}

public class PlanetPrisonEvent
{
    public int Id { get; set; }
    public int MinParticipants { get; set; }
    public int CurrentParticipants { get; set; }
    public int ConfirmedParticipants { get; set; }
    public bool IsActive { get; set; }
    public TimeSpan StartTime { get; set; }
    public Dictionary<string, List<NetUserId>> RoleCandidates { get; set; } = new();
    public Dictionary<string, NetUserId> FinalWinners { get; set; } = new();
}
// Sunrise-End

