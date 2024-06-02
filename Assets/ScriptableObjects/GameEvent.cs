using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    List<GameEventListeners> listeners = new List<GameEventListeners>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }
    public void Register(GameEventListeners GameEventListener)
    {
        listeners.Add(GameEventListener);
    }
    public void Unregister(GameEventListeners GameEventListener)
    {
        listeners.Remove(GameEventListener);
    }

}
