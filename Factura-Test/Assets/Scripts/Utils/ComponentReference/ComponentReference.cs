using UnityEngine;

namespace Game.Scripts.ComponentReference
{
    [CreateAssetMenu(menuName = "ComponentReference")]
    public class ComponentReference : ScriptableObject
    {
        [SerializeField] private Component _reference;

        public Component Reference => _reference;

        public bool HasReference => _reference != null;

        public void SetReference(Component component) => _reference = component;

        public T GetReference<T>()
        {
            if (_reference is T castedRef) return castedRef;
            return default;
        }
        
        public bool TryGetReference<T>(out T reference)
        {
            reference = default;
            if (!HasReference) return false;

            if (_reference is not T castedRef) return false;
            
            reference = castedRef;
            return true;
        }
    }
}
