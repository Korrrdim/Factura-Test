using System;
using UnityEngine;

namespace Game.Scripts.ComponentReference
{
    public class ComponentReferenceLink : MonoBehaviour
    {
        [SerializeField] private Component _target;
        [SerializeField] private ComponentReference _source;
        [SerializeField] private bool _isAutoReference = true;

        public void SetReference() => _source.SetReference(_target);
        private void OnEnable()
        {
            if (_isAutoReference) SetReference();
        }
    }
}
