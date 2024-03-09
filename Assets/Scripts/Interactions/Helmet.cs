using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Interctable
{
    public class Helmet : MonoBehaviour, IInteractable
    {
        [SerializeField] private ParticleIndicator _particle;

        private Vector3 _initPos;
        private Quaternion _initRot;
        private bool _canInteract; 
        private XRBaseInteractable _interactable;

        private void OnEnable()
        {
            if (_interactable == null)
                TryGetComponent(out _interactable);

            _interactable.selectEntered.AddListener(Interacting);
            _interactable.selectExited.AddListener(Deselect);
        }
        private void OnDisable()
        {
            _interactable.selectEntered.RemoveAllListeners();
            _interactable.selectExited.RemoveAllListeners();
        }

        private void Awake()
        {
            _initPos = transform.position;
            _initRot = transform.rotation; 
        }

        private void Start()
        {
            _canInteract = true;

            if (_canInteract) _particle.StartIndicator();
        }

        public void Interact()
        {
            if (!_canInteract) return;
            _canInteract = false; 

            _particle.StopIndicator();
        }

        private void Interacting(BaseInteractionEventArgs e)
        {
            Interact();
        }

        private void Deselect(BaseInteractionEventArgs e)
        {
            ResetAction();
        }

        private void ResetAction()
        {  
            transform.SetPositionAndRotation(_initPos, _initRot);

            _particle.StartIndicator();
            
            _canInteract = true;
        } 
    }
}