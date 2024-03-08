using UnityEngine;

namespace Dialogue
{
    public class ParticleIndicator : TalkIndicator
    {
        [SerializeField] private ParticleSystem _particles;
        [SerializeField] private bool _loop;
        public override void StartIndicator()
        {
            var main = _particles.main;
            main.loop = _loop;

            _particles.Play();
        }

        public override void StopIndicator()
        {
            _particles.Stop();
        }
    }
}