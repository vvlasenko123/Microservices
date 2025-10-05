using Core.Domain.Common.Interfaces;

namespace Core.Domain.ProductOrder.Events;

internal class DomainEvent
{
    private static readonly List<IDomainEvent> _events = new();
    public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

    internal static void AddEvent(IDomainEvent e)
    {
        _events.Add(e);
    }
}