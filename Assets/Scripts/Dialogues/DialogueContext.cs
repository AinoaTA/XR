using UnityEngine;
using TMPro; 
using System.Linq;

namespace Dialogue
{
    public class DialogueContext : MonoBehaviour
    {
        [SerializeField] private GameObject _contextPanel;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Transform _contextButtons;
        [SerializeField] private ButtonOption[] _buttonOptions;

        public void Enabled(bool enabled)
        {
            _contextPanel.SetActive(enabled);
            //Clear();
        }

        /// <summary>
        /// Writes NPC dialogue.
        /// </summary>
        /// <param name="t"></param>
        public void WriteContext(string t)
        {
            _text.text = t;
        }

        /// <summary>
        /// Enable necessary buttons and set up.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="s"></param>
        public void WriteButton(DialogueOptions options, DialogueSystem s)
        {
            for (int i = 0; i < options.AllOptions.Length; i++)
            { 
                ButtonOption b = _buttonOptions[i];
                b.gameObject.SetActive(true);
                b.Init(options.AllOptions[i].Sequence, i, options.AllOptions[i].WasSelected, s);
            }
        }

        public void Clear()
        {
            _text.ClearMesh();

            _buttonOptions.ToList().ForEach(n => n.gameObject.SetActive(false));
        }
    }
}