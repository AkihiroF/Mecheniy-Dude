using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Source.Interactable
{
    public class PlayerInteractiveComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask interactiveLayer;
        private List<IInteractiveObject> _objectsInRange;

        private void Awake()
        {
            _objectsInRange = new List<IInteractiveObject>();
        }

        public void GetItem()
        {
            if(_objectsInRange.Count == 0)
                return;
            var currentObj = _objectsInRange[0];
            _objectsInRange.RemoveAt(0);
            currentObj.Interact();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((interactiveLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                _objectsInRange.Add(other.GetComponent<IInteractiveObject>());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == interactiveLayer)
            {
                _objectsInRange.Remove(other.GetComponent<IInteractiveObject>());
            }
        }
    }
}