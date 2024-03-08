using Interctable;
using UnityEngine;
using UnityEngine.Events;

namespace NPC
{
    public class NPCInteractable : NPC, IInteractable
    {
        [SerializeField] protected UnityEvent _interactDialogue;

        public void Interact()
        {
            if (_isPlayer)
                _interactDialogue?.Invoke();
        }
    }
}