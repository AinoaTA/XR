using UnityEngine;

namespace Common
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _music;

        private void Start()
        {
            ManagerSound.Instance.PlayMusic(_music);
        }
    }
}