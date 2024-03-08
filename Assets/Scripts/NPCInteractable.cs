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
            Inputs.PlayerInputs.OnInteraction += Interact;
        }

        private void OnDisable()
        {
            Inputs.PlayerInputs.OnInteraction -= Interact;
        }

        public void Interact()
        {
            if (_isPlayer)
                _interactDialogue?.Invoke();
        }
    }
}