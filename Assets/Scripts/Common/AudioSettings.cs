using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Common
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private GameObject _audioSettings;
        [SerializeField] private AudioMixer TheMixer;
        [SerializeField] private Slider musicSlider, sfxSlider, dialogueSlider; 

        private void Start()
        {
            SetMusicVolume();
            SetSFXVolume();
            SetDialoguesVolume();
        }

        public void AudioSettingsMenu(InputAction.CallbackContext ctx) 
        {
            if (ctx.performed)
            { 
                EnableAudioSettings();
            }
        }

        private void EnableAudioSettings() 
        { 
            _audioSettings.SetActive(!_audioSettings.activeSelf);
        }

        public void SetDialoguesVolume()
        { 
            TheMixer.SetFloat("MusicValue", musicSlider.value);
        }
        public void SetMusicVolume()
        { 
            TheMixer.SetFloat("MusicValue", musicSlider.value);
        }

        public void SetSFXVolume()
        { 
            TheMixer.SetFloat("SFXValue", (sfxSlider.value));
        }
    }
}