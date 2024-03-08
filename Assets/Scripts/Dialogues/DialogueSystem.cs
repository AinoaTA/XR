using System.Collections;
using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueSystem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DialogueContext _dialogueContext;
        [SerializeField] private Dialogue _dialogue;

        private AudioSource _audioSource;

        private bool _stopped;
        private IEnumerator _routine;
        private void Awake()
        {
            TryGetComponent(out _audioSource);
        }

        public void ResumeDialogue()
        {
            _audioSource.UnPause();
            _stopped = false;

            StartDialogue(_dialogue);
        }

        public void StartDialogue(Dialogue d)
        {
            _dialogue = d;

            if (_routine != null) StopCoroutine(_routine);
            StartCoroutine(_routine = WriteRoutine());
        }

        public void PauseDialogue()
        {
            _stopped = true;
        }
        private IEnumerator WriteRoutine()
        {
            _dialogueContext.Write(_dialogue.Conver.Sentence);
            float time = 6;
            float t = 0;
            if (_dialogue.Conver.Voice != null)
            {
                _audioSource.clip = _dialogue.Conver.Voice;
                _audioSource.Play();
                time = _audioSource.clip.length;
                Debug.Log(_audioSource.isPlaying);
            }

            while (t < time)
            {
                t += Time.deltaTime;
                yield return new WaitWhile(() => _stopped);
            }

            if (_dialogue.Conver.NextDialogue != null)
                StartDialogue(_dialogue.Conver.NextDialogue);
            else
                _dialogueContext.Clear(); 
        }
    }
}