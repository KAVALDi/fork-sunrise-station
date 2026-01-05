using Content.Shared._Sunrise.Events;
using Robust.Shared.Network;
using Robust.Server.Player;

namespace Content.Server._Sunrise.Ghost.Events;

public partial class PlanetPrisonEventSystem
{
    public void HandleConfirmPrisonEventWinners(ICommonSession sender, ConfirmPrisonEventWinnersMessage msg)
    {
        if (!_activeEvents.TryGetValue(msg.EventId, out var evt))
            return;
        // Обновляем кандидатов
        evt.RoleCandidates = msg.RoleCandidates;
        // Проверим, все ли участники подтвердили или отказались (или по таймеру)
        // ...здесь должен быть продвинутый чек
        var ready = AreAllCandidatesReady(evt.RoleCandidates); // реализовать
        if (ready)
        {
            DoWinnerRollAndStart(msg.EventId, evt);
        }
        // иначе — ждем дальнейших подтверждений
    }
    
    private bool AreAllCandidatesReady(Dictionary<string, List<NetUserId>> roleCandidates)
    {
        // Реализация проверки: макет (требует доработки на клиенте, чтобы передавался статусы)
        return true;
    }
    
    private void DoWinnerRollAndStart(int eventId, PlanetPrisonEvent evt)
    {
        var final = new Dictionary<string, NetUserId>();
        // РАЗБОР КАНДИДАТОВ
        foreach (var (role, users) in evt.RoleCandidates)
        {
            if (users.Count == 1)
            {
                final[role] = users[0];
            }
            else if (users.Count > 1)
            {
                // Случайный выбор из high/medium (на практике разбить по приоритетам HIGH>medium>Low)
                // Для MVP: просто случайно
                final[role] = users[Rand.Next(users.Count)];
            }
        }
        evt.FinalWinners = final;
        StartEvent(eventId); // уже реализовано.
        // Push уведомления
        PushWinnersNotify(evt);
    }

    private void PushWinnersNotify(PlanetPrisonEvent evt)
    {
        // Реализовать push через сетевое сообщение или EUI отправку победителям и проигравшим (заглушка)
        // foreach (var winner in evt.FinalWinners) ...
    }
}

