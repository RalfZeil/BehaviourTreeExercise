using System;
using System.Collections.Generic;

public enum EventType
{
    SpottedPlayer = 0,
    UnspottedPlayer = 1,
    PlayerDied = 2,
}

public static class EventManager
{
    private static readonly Dictionary<EventType, Action> eventDictionary = new();

    public static void AddListener(EventType eventType, Action action)
    {
        if (!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary.Add(eventType, null);
        }

        eventDictionary[eventType] -= action;
        eventDictionary[eventType] += action;
    }

    public static void RemoveListener(EventType eventType, Action action)
    {
        if (eventDictionary.ContainsKey(eventType) && eventDictionary[eventType] != null)
        {
            eventDictionary[eventType] -= action;
        }
    }

    public static void InvokeEvent(EventType eventType)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType]?.Invoke();
        }
    }
}
