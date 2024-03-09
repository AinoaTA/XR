using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Interctable
{
    public class Helmet : MonoBehaviour, IInteractable
    {
        [SerializeField] private ParticleIndicator _particle;
        [SerializeField] private NPC.NPCTriggerAnswer _npc;
        [SerializeField] private bool _goodChoise;

        private XRBaseInteractable _interactableGrab; 
        //[SerializeField] private Common.Attacher _attacher;

        private Vector3 _initPos;
        private Quaternion _initRot;
        private bool _canInteract; 
[SerializeField]        private Common.Attacher _attacher;

        private bool _grabbing;
        private bool _setup;

        private void OnEnable()
        {
            if (_interactableGrab == null)
                TryGetComponent(out _interactableGrab);

            _interactableGrab.selectEntered.AddListener(Interacting);
            _interactableGrab.selectExited.AddListener(Deselect);

            _interactableGrab.activated.AddListener(ActivateAction);
            _interactableGrab.deactivated.AddListener(DeactivateAction);
        }
        private void OnDisable()
        {
            _interactableGrab.selectEntered.RemoveAllListeners();
            _interactableGrab.selectExited.RemoveAllListeners();

            _interactableGrab.activated.RemoveAllListeners();
            _interactableGrab.deactivated.RemoveAllListeners();
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

            Debug.Log("interacting");
            
            _particle.StopIndicator();
        }

        private void ActivateAction(BaseInteractionEventArgs e)
        {
            Debug.Log("Traing attachment"); 
            //_attacher = e.interactorObject.transform.getco<Common.Attacher>();
            Debug.Log("_attacher: " + _attacher);
            if (_attacher != null)
            {
                _attacher.Attach(transform, Vector3.zero, Vector3.zero, ResetAction);
                var g = e.interactorObject as XRRayInteractor;
                g.attachTransform = _attacher.ToAttach;

                _npc.TestHelmet(_goodChoise);
            }
        }

        private void DeactivateAction(BaseInteractionEventArgs e)
        {
            if (_attacher != null)
                _attacher.Dettach();
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