using System;
using UnityEngine;

namespace Common
{
    public class Attacher : MonoBehaviour
    {
        [SerializeField] private Transform _PointAttach;

        private Transform _previousParent;
        private Transform _attached;
        private Action _toReset; 
        public void Attach(Transform p, Vector3 pos, Vector3 euler, Action reset=null) 
        {
            _previousParent = p.parent;
            _attached = p;
            _attached.SetParent( _PointAttach);
            _attached.localPosition = pos;
            _attached.localRotation = Quaternion.Euler(euler);
            _toReset = reset; 
        }

        public void Dettach() 
        {
            _attached.SetParent(_previousParent);

            if(_toReset!= null)
            _toReset?.Invoke();
        }
    }
}