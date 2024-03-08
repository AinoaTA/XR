using UnityEngine;
using UnityEngine.Events;

namespace NPC
{
    public class NPC : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] private UnityEvent _triggerPlayer;
        
        private bool _canInteract;
        protected bool _isPlayer;

        protected virtual void Start()
        {
            _canInteract = true; 
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!_canInteract) return; 

            if (other.CompareTag("Player"))
            {
                _isPlayer = true;
                _triggerPlayer?.Invoke();
            }
            else
                _isPlayer = false;
        }  
    }
}