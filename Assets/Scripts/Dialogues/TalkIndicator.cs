using UnityEngine;

namespace Dialogue
{
    public abstract class TalkIndicator : MonoBehaviour
    {
        public abstract void StartIndicator();
        public abstract void StopIndicator();
    }
}