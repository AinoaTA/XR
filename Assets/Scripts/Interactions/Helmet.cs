using UnityEngine;

namespace Interctable
{
    public class Helmet : MonoBehaviour, IInteractable
    {
        private Vector3 _initPos;
        private Quaternion _initRot;

        private bool _canInteract;

        [SerializeField] private ParticleIndicator _particle;

        private void Awake()
        {
            _initPos = transform.position;
            _initRot = transform.rotation;
        }

        private void Start()
        {
            _canInteract = true;

            if (_canInteract)
                _particle.StartIndicator();
        }

        public void Interact()
        {
            if (!_canInteract) return;
            _canInteract = false;
            _particle.StopIndicator();
        }

        private void ResetAction()
        {
            transform.position = _initPos;
            transform.rotation = _initRot;
            _canInteract = true;
            _particle.StartIndicator();
        }
    }
}