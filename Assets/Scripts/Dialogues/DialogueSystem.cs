using System;
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
        [SerializeField] private Transform _parentButton;
        [SerializeField] private ButtonOption _buttonOption;

        private Dialogue _dialogue;
        private DialogueOptions _dialogueOptions;
        private int _indexOption;
        private AudioSource _audioSource;
        private bool _stopped;
        private IEnumerator _routine;
        private bool _requiresPlayerAnswer;

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
            //StartDialogue(_dialogueOptions.AllOptions[i].Dialogue);
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

            //wait if npc must wait for player question
            if (_requiresPlayerAnswer)
            {
                yield return new WaitWhile(() => _requiresPlayerAnswer);
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

            var nextDialogue = _dialogue.Conver.NextDialogue;
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