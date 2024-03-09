using System.Collections; 
using UnityEngine;

namespace Dialogue
{
    public class LightIndicator : TalkIndicator
    {
        [SerializeField] private Light _light;
        [SerializeField] private float _maxItensity = 5;
        private IEnumerator _routine;
        public override void StartIndicator()
        {
            if (_routine != null) StopCoroutine(_routine);
            StartCoroutine(_routine = EnableLigth(true));
        }

        public override void StopIndicator()
        {
            if (_routine != null) StopCoroutine(_routine);
            StartCoroutine(_routine = EnableLigth(false));
        }

        private IEnumerator EnableLigth(bool enable)
        {
            float currentIntensity = _light.intensity;
            float nextValue = enable ? _maxItensity : 0;

            float t = 0;
            float max = 1;

            while (t <= max)
            {
                t += Time.deltaTime;
                _light.intensity = Mathf.Lerp(currentIntensity, nextValue, t / max);
                yield return null;
            }
        }
    }
}