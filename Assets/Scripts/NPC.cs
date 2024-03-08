using UnityEngine;
using UnityEngine.Events;

namespace NPC
{
    public class NPC : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] private UnityEvent _interact;

        private bool _canInteract;

        private void Start()
        {
            _canInteract = true;

            _interact?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_canInteract) return;

            if (other.CompareTag("Player"))
            {
                _interact?.Invoke();
            }
        }
    }
}