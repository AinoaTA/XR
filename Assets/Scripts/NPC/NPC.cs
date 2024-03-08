using UnityEngine;
using UnityEngine.Events;

namespace NPC
{
    public class NPC : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] private UnityEvent _triggerPlayer;

        protected bool _isPlayer;

        protected virtual void OnTriggerEnter(Collider other)
        {
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