using TMPro;
using UnityEngine;

namespace Dialogue
{
    public class ButtonOption : MonoBehaviour
    {
        [SerializeField] private Color _pressedColor;
        [SerializeField] private TMP_Text _text;

        private DialogueSystem _system; 
        private int _index;
        public void Init(string t, int i, DialogueSystem system)
        {
            _text.text = t;
            _index = i;
            _system = system;
        }

        public void Pressed()
        {
            _system.AnswerOptions(_index);
        }
    }
}