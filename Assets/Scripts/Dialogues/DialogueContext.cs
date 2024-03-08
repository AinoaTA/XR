using UnityEngine;
using TMPro;

namespace Dialogue
{
    public class DialogueContext : MonoBehaviour
    {
        [SerializeField] private GameObject _contextCanvas;
        [SerializeField] private TMP_Text _text;

        public void Enabled(bool enabled)
        { 
            _contextCanvas.SetActive(enabled);
            _text.ClearMesh();
        }

        public void Write(string t)
        { 
            _text.text = t;
        }

        public void Clear() 
        {
            _text.ClearMesh();
        }
    }
}