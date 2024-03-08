using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueSystem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DialogueContext _dialogueContext;
        [SerializeField] private TalkIndicator _indicator;

        private Dialogue _dialogue;
        private AudioSource _audioSource;
        private bool _stopped;
        private IEnumerator _routine;

        //private Queue<Dialogue> _dialoguesQueue = new();
        private void Awake()
        {
            TryGetComponent(out _audioSource);
        }
        /// <summary>
        /// Continue dialogue.
        /// </summary>
        public void ResumeDialogue()
        {
            _audioSource.UnPause();
            _stopped = false;

            StartDialogue(_dialogue);
        }

        /// <summary>
        /// Starts new dialogue
        /// </summary>
        /// <param name="d"> new dialogue </param>
        public void StartDialogue(Dialogue d)
        {
            //if new dialogue has lower priority or equals, it won't play (low priority = trigger dialogues)
            if (_dialogue != null)
            {
                if (d.Conver.Priority <= _dialogue.Conver.Priority) return;
                //else if (d.Conver.Priority == _dialogue.Conver.Priority)
                //    _dialoguesQueue.Enqueue(d);
            }

            _dialogue = d;
            _indicator.StartIndicator(); //visual feedback to show npc is talking.

            if (_routine != null) StopCoroutine(_routine);
            StartCoroutine(_routine = WriteRoutine());
        }

        public void PauseDialogue()
        {
            _stopped = true;
            _indicator.StopIndicator();
        }

        private IEnumerator WriteRoutine()
        {
            _dialogueContext.Enabled(true);
            _dialogueContext.Write(_dialogue.Conver.Sentence);
            float time = 6;
            float t = 0;
            //check if conver has voice to play it
            if (_dialogue.Conver.Voice != null)
            {
                _audioSource.clip = _dialogue.Conver.Voice;
                _audioSource.Play();
                time = _audioSource.clip.length;
            }

            //do this for interrupt conversation if is necessary
            while (t < time)
            {
                t += Time.deltaTime;
                yield return new WaitWhile(() => _stopped);
            }

            //check if there is next conver or close
            if (_dialogue.Conver.NextDialogue != null)
                StartDialogue(_dialogue.Conver.NextDialogue);
            else
            {
                //if (_dialoguesQueue.Count != 0) 
                //{
                //    _dialogue = _dialoguesQueue.Dequeue();
                //    StartDialogue(_dialogue);
                //    yield break;
                //}

                _indicator.StopIndicator();
                _dialogueContext.Enabled(false);
                _dialogue = null;
            }
        }
    }
}