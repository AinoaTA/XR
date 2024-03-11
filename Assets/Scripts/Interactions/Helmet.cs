using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Interctable
{
    public class Helmet : MonoBehaviour, IInteractable
    {
        [SerializeField] private ParticleIndicator _particle;
        [SerializeField] private NPC.NPCTriggerAnswer _npc;
        [SerializeField] private bool _goodChoise;
        [Header("sfx")]
        [SerializeField] private AudioClip _grab;
        [SerializeField] private AudioClip _wearHelmet;

        private XRBaseInteractable _interactableGrab; 

        private Vector3 _initPos;
        private Quaternion _initRot;
        private bool _canInteract;
        [SerializeField] private Common.Attacher _attacher;

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

            if (_grab != null)
                ManagerSound.Instance.PlaySound(_grab, transform.position);

            _canInteract = false;

            _particle.StopIndicator();
        }

        private void ActivateAction(BaseInteractionEventArgs e)
        {
            if (_attacher != null)
            {
                _attacher.Attach(transform, Vector3.zero, Vector3.zero, ResetAction);
                var g = e.interactorObject as XRRayInteractor;
                g.attachTransform = _attacher.ToAttach;

                if(_wearHelmet!=null)
                ManagerSound.Instance.PlaySound(_wearHelmet, transform.position);
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