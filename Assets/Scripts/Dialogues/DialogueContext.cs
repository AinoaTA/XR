using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Dialogue
{
    public class DialogueContext : MonoBehaviour
    {
        [SerializeField] private GameObject _contextPanel;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Transform _contextButtons;
        [SerializeField] private ButtonOption _buttonOptions;
         
        private List<ButtonOption> _buttons = new();
 
        public void Enabled(bool enabled)
        {
            _contextPanel.SetActive(enabled);
            Clear();
        }

        public void Write(string t)
        {
            _text.text = t;
        }

        public void WriteButton(DialogueOptions options, DialogueSystem s)
        {
            for (int i = 0; i < options.AllOptions.Length; i++)
            {
                ButtonOption b = Instantiate(_buttonOptions, _contextButtons);
                b.Init(options.AllOptions[i].Sequence, i, s);
            } 
        }

        public void Clear()
        {
            _text.ClearMesh();

            for (int i = 0; i < _buttons.Count; i++)
            {
                Destroy(_buttons[i].gameObject);
            } 
        }
    }
}