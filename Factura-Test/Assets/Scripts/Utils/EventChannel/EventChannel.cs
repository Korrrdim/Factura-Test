using System;
using UnityEngine;

namespace Game.Scripts.Events
{
    public abstract class EventChannel<T> : ScriptableObject
    {
        public event Action<T> OnEvent;

        public void Invoke(T obj)
        {
            OnEvent?.Invoke(obj);
        }
    
    }
}
