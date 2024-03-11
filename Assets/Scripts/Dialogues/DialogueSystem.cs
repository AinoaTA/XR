using System.Collections;
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
        private DialogueOptions _dialogueOptions;
        private AudioSource _audioSource;
        private IEnumerator _routine;

        private int _indexOption;
        private bool _requiresPlayerAnswer;
        private bool _stopped;

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
            if (d == null) return;
            //if new dialogue has lower priority or equals, it won't play (low priority = trigger dialogues)
            if (_dialogue != null)
            {
                if (_dialogue.Conver.ID == d.Conver.ID) return;

                if (d.Conver.Priority < _dialogue.Conver.Priority) return;
            }

            _dialogue = d;
            _indicator.StartIndicator(); //visual feedback to show npc is talking.

            if (_dialogue is DialogueOptions)
            {
                _requiresPlayerAnswer = true;

                _dialogueOptions = _dialogue as DialogueOptions;

                _dialogueContext.WriteButton(_dialogueOptions, this);
            }

            if (_routine != null) StopCoroutine(_routine);
            StartCoroutine(_routine = WriteRoutine());
        }

        public void AnswerOptions(int i)
        {
            _indexOption = i;
            _requiresPlayerAnswer = false;
            _dialogueContext.HideButtons(); 
        }

        public void PauseDialogue()
        {
            _stopped = true;
            _indicator.StopIndicator();
        }

        public void StopDialogue()
        {
            _indicator.StopIndicator();
            _dialogue = null;
        }

        private IEnumerator WriteRoutine()
        {
            _stopped = false;

            _dialogueContext.Enabled(true);
            _dialogueContext.WriteContext(_dialogue.Conver.Sentence);
            float time = 6;
            float t = 0;

            //check if conver has voice to play it
            if (_dialogue.Conver.Voice != null)
            {
                _audioSource.clip = _dialogue.Conver.Voice;
                _audioSource.Play();
                time = _audioSource.clip.length;
            }

            //wait if npc must wait for player question
            if (_requiresPlayerAnswer)
            { 
                yield return new WaitWhile(() => _requiresPlayerAnswer);
                _dialogueOptions.AllOptions[_indexOption].WasSelected = true;
                _dialogue = _dialogueOptions.AllOptions[_indexOption].Dialogue; 
            }
            else
            {
                //do this for interrupt conversation if is necessary. Only when npc speaks and no waits for player question.
                while (t < time)
                {
                    t += Time.deltaTime;
                    yield return new WaitWhile(() => _stopped);
                }
            }


            Dialogue nextDialogue = null;

            if (_dialogueOptions == null)
                nextDialogue = _dialogue.Conver.NextDialogue;
            else
                nextDialogue = _dialogue;

            _dialogueOptions = null;
            _dialogue = null;
            //check if there is next conver or close
            if (nextDialogue != null)
            {
                StartDialogue(nextDialogue);
            }
            else
            {
                _indicator.StopIndicator();
                _dialogueContext.Enabled(false);
            }
        }
    }
}