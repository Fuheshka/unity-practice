using System;
using System.Collections.Generic;

/// <summary>
/// Simple event bus for managing events and subscribers
/// </summary>
public static class EventBus {

    /// <summary>
    /// Dictionary to hold event subscribers
    /// </summary>
    static readonly Dictionary<Type, List<Delegate>> _events = new Dictionary<Type, List<Delegate>>();

    /// <summary>
    /// Subscribe to an event
    /// </summary>
    /// <param name="callback">Callback to invoke when the event is published</param>
    public static void Subscribe<T>(Action<T> callback) {
        if (!_events.TryGetValue(typeof(T), out var subscribers)) {
            subscribers = new List<Delegate>();
            _events[typeof(T)] = subscribers;
        }
        subscribers.Add(callback);
    }

    /// <summary>
    /// Unsubscribe from an event
    /// </summary>
    /// <param name="callback">Callback to remove from the event</param>
    public static void Unsubscribe<T>(Action<T> callback) {
        if (_events.TryGetValue(typeof(T), out var subscribers)) {
            subscribers.Remove(callback);
        }
    }

    /// <summary>
    /// Publish an event
    /// </summary>
    /// <param name="data">Event data to publish</param>
    public static void Publish<T>(T data) {
        if (_events.TryGetValue(typeof(T), out var subscribers)) {
            foreach (var sub in subscribers) {
                (sub as Action<T>)?.Invoke(data);
            }
        }
    }
}
