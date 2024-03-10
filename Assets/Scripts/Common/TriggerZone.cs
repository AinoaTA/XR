using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private bool _destroyAfterTriggered;

        [SerializeField] private UnityEvent _triggerEvents;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
            {
                _triggerEvents?.Invoke();

                if (_destroyAfterTriggered)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}