using UnityEngine.InputSystem;
using UnityEngine;

namespace Inputs
{
    public class PlayerInputs : MonoBehaviour
    {
        public delegate void DelegateOnInteract();
        public static DelegateOnInteract OnInteraction;

        public void Interaction(InputAction.CallbackContext ctx)
        {
            switch (ctx.phase)
            {
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Started:
                    OnInteraction?.Invoke();
                    break;
                case InputActionPhase.Performed:
                    break;
                case InputActionPhase.Canceled:
                    break; 
            }
        }
    }
}