 using PlatformerMvc.Utils;
 using UnityEngine;


namespace PlatformerMvc
{
    public sealed class CameraController : IUpdate
    {
        private readonly Camera _camera;
        private readonly Transform _view;

        public CameraController(Camera camera, Transform view)
        {
            _camera = camera;
            _view = view;
        }

        public void Update()
        {
            _camera.transform.position = _camera.transform.position.Change(_view.transform.position.x);
        }
    }
}