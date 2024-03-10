using UnityEngine;

namespace Common
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _music;
 
        public void StartGame()
        {
            ManagerSound.Instance.PlayMusic(_music);
        }
    }
}