using System.Collections.Generic;

namespace ResultStudioWPF.Domain.DDD
{

  /// <summary> An aggregate root base class. Explicitly mark aggregate roots and implement their common logic </summary>
  public abstract class AggregateRoot : Entity
  {
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    protected virtual void AddDomainEvent(IDomainEvent newEvent)
    {
      _domainEvents.Add(newEvent);
    }

    public virtual void ClearEvents()
    {
      _domainEvents.Clear();
    }
  }
}
