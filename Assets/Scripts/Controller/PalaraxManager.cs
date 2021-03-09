using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMvc
{
    public sealed class PalaraxManager : IUpdate, IInit
    {
        private readonly BackgroundData _data;
        private readonly Transform _camera;
        private readonly float _coef;
        private List<Transform> _back = new List<Transform>();
        private Vector3 _backStartPosition;
        private Vector3 _cameraStartPorition;

        public PalaraxManager(BackgroundData data, Transform camera)
        {
            _data = data;
            _camera = camera;
            _coef = _data._coeficientMovingBackground;
            _backStartPosition = _data._offsetBackground;
            _cameraStartPorition = _data._cameraStartPosition;
        }

        public void Init()
        {
            foreach (var sprite in _data._groundLayers.Layers)
            {
                var layer = Object.Instantiate(sprite, Vector3.zero, Quaternion.identity);
                _back.Add(layer);
            }

            // _back._root = Object.Instantiate(_data._backGround._root, Vector3.zero, Quaternion.identity);
        }

        public void Update()
        {
            var coefDelta = 1.0f;
            
            foreach (var transform in _back)
            {
                var newPosition = _backStartPosition + (_camera.position - _cameraStartPorition) * _coef * coefDelta;
                transform.position = new Vector3(newPosition.x, newPosition.y, 0.0f);
                coefDelta *= _coef;
            }

            
            // _back._sky. = _backStartPosition + (_camera.position - _cameraStartPorition) * _coef;
        }
    }
}