using UnityEngine;
namespace NPC
{
    public class NPCTriggerAnswer : NPC
    {
        [SerializeField] private Dialogue.Dialogue _triggerFirstDialogue;
        [SerializeField] private Dialogue.Dialogue _triggerDefaultDialogue;

        [SerializeField] private Dialogue.Dialogue[] _goodDialogue;
        [SerializeField] private Dialogue.Dialogue[] _badDialogue;

        [SerializeField] private Dialogue.Dialogue _exitBadDialogue;
        [SerializeField] private Dialogue.Dialogue _exitGoodDialogue;

        private int _indexesGood;
        private int _indexesBad;

        private bool _firstTime = true;

        private Dialogue.DialogueSystem _dialogueSystem;
        private bool _goodAnswerBool;

        private void Awake()
        {
            TryGetComponent(out _dialogueSystem);
        }
        public void TriggerClip()
        {
            if (_firstTime)
            {
                _dialogueSystem.StartDialogue(_triggerFirstDialogue);
            }
            else
                _dialogueSystem.StartDialogue(_triggerDefaultDialogue);
        }

        public void TestHelmet(bool good) 
        {
            if (good)
            {
                _dialogueSystem.StartDialogue(_goodDialogue[_indexesGood]);
                _indexesGood++;

                if (_indexesGood >= _goodDialogue.Length)
                    _indexesGood = 0;

                _goodAnswerBool = true;
            }
            else
            {
                _dialogueSystem.StartDialogue(_badDialogue[_indexesBad]);
                _indexesBad++;

                if (_indexesBad >= _badDialogue.Length)
                    _indexesBad = 0;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_goodAnswerBool)
                _dialogueSystem.StartDialogue(_exitBadDialogue);
            else
                _dialogueSystem.StartDialogue(_exitGoodDialogue);
        }
    }
}