﻿namespace Core.Domain.Common.Interfaces;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}