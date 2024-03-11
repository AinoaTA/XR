using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Dialogue
{
    public class ButtonOption : MonoBehaviour
    {
        [SerializeField] private Color _pressedColor;
        [SerializeField] private TMP_Text _text;

        private Image _image;
        private DialogueSystem _system;
        private int _index;
        private Color _reset = Color.white;

        private void Awake()
        {
            TryGetComponent(out _image);
        }

        public void Init(string t, int i, bool pressedBefore, DialogueSystem system)
        {
            _text.text = t;
            _index = i;
            _system = system; 
            _image.color = pressedBefore ? _pressedColor : _reset;
        }

        public void Pressed()
        {
            _system.AnswerOptions(_index);
        }
    }
}