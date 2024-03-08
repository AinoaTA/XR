using UnityEngine.InputSystem;
using UnityEngine;

namespace Inputs
{
    public class ManagerInputs : MonoBehaviour
    {
        public delegate void DelegateOnInteract();
        public static DelegateOnInteract OnInteraction;

        public delegate void DelegateOnMove(Vector2 dir);
        public static DelegateOnMove OnMovement;

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

        public void Movement(InputAction.CallbackContext ctx) 
        {
            OnMovement?.Invoke(ctx.ReadValue<Vector2>());
        }
    }
}