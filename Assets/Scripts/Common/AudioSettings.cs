using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Common
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer TheMixer;
        [SerializeField] private Slider musicSlider, sfxSlider, dialogueSlider;
        //[SerializeField] private TMP_Text musicLabel, sfxLabel, dialogueLabel;

        private void Start()
        {
            SetMusicVolume();
            SetSFXVolume();
            SetDialoguesVolume();
        }
        public void SetDialoguesVolume()
        {
            //dialogueLabel.text = (dialogueSlider.value +20).ToString();
            //TheMixer.SetFloat("MasterValue", 0); 
            TheMixer.SetFloat("MusicValue", musicSlider.value);
        }
        public void SetMusicVolume()
        {
            //musicLabel.text = (musicSlider.value + 20 ).ToString();
            TheMixer.SetFloat("MusicValue", musicSlider.value);
        }

        public void SetSFXVolume()
        {
            //sfxLabel.text = (sfxSlider.value +20).ToString();
            TheMixer.SetFloat("SFXValue", (sfxSlider.value));
        }
    }
}