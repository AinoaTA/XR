using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogues/Basic Dialogue")]
    public class Dialogue : ScriptableObject
    {
        internal DialogueInfo Conver => _dialogues;
        [SerializeField] private DialogueInfo _dialogues; 
    }

    [System.Serializable]
    internal struct DialogueInfo
    {
        public string Sentence => _sentence;
        public AudioClip Voice => _voice;
        public Dialogue NextDialogue => _nextDialogue;


        [SerializeField] private string _sentence;
        [SerializeField] private AudioClip _voice;
        [SerializeField] private Dialogue _nextDialogue;
    }
}