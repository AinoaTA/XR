using System.Collections;
using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private Dialogue _startDialogue { get => _currentDialogue; }

        private Dialogue _currentDialogue;
        private AudioSource _audioSource;

        private bool _stopped;
        private void Awake()
        {
            TryGetComponent(out _audioSource);
        }

        public void ResumeDialogue()
        {
            _audioSource.UnPause();
            _stopped = false;
        }

        public void StartDialogue(Dialogue d)
        {
            _currentDialogue = d;
        }

        private IEnumerator WriteRoutine()
        {
            yield return null;

            yield return new WaitWhile(() => _stopped);
        }
    }
}