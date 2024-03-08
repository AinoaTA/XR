using Interctable;
using UnityEngine;
using UnityEngine.Events;

namespace NPC
{
    public class NPCInteractable : NPC, IInteractable
    {
        [SerializeField] protected UnityEvent _interactDialogue;
        private void OnEnable()
        {
            Inputs.ManagerInputs.OnInteraction += Interact;
        }

        private void OnDisable()
        {
            Inputs.ManagerInputs.OnInteraction -= Interact;
        }

        public void Interact()
        {
            if (_isPlayer)
                _interactDialogue?.Invoke();
        }
    }
}