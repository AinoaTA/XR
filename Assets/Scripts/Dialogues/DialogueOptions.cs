using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "option_Dialogue", menuName = "Dialogues/Option Dialogue")]
    public class DialogueOptions : Dialogue
    {
        internal Options[] AllOptions => _options;

        [SerializeField] private Options[] _options;
        [System.Serializable]
        internal struct Options
        {
            public string Sequence => _sequence;
            public Dialogue Dialogue => _nextConver;

            public bool WasSelected { get => _wasSelected; set => _wasSelected = value; }

            [SerializeField] private bool _wasSelected;
            [SerializeField] private string _sequence;
            [SerializeField] private Dialogue _nextConver;
        }
    }
}