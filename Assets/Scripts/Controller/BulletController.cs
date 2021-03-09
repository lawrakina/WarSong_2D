using PlatformerMvc.Utils;
using UnityEngine;


namespace PlatformerMvc
{
    public class BulletController
    {
        private float _radius = 0.08f;
        private Vector3 _velocity;

        private float _groundLevel = -3;
        private float _g = -10;

        private LevelObjectView _view;
        private bool _startPos;

        public BulletController(LevelObjectView view)
        {
            _view = view;
            Active(false);
        }

        // public void Update()
        // {
        //     if (IsGrounded())
        //     {
        //         SetVelocity(_velocity.Change(y: -_velocity.y));
        //         _view._transform.position = _view._transform.position.Change(y: _groundLevel + _radius);
        //     }
        //     else
        //     {
        //         SetVelocity(_velocity + Vector3.up * _g * Time.deltaTime);
        //         _view._transform.position += _velocity * Time.deltaTime;
        //         if(_startPos)
        //         {
        //             Active(true);
        //         }
        //     }
        // }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            _view._transform.position = position;
            SetVelocity(velocity);
            _startPos = true;
        }

        public void Active(bool val)
        {
            _view._trail.enabled = val;
            _view.gameObject.SetActive(val);
            _startPos = false;
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            var angle = Vector3.Angle(Vector3.left, _velocity);
            var axis = Vector3.Cross(Vector3.left, _velocity);
            _view._transform.rotation = Quaternion.AngleAxis(angle, axis);
            Active(true);
            _view._rigidbody2d.AddForce(velocity, ForceMode2D.Impulse);
        }

        // private bool IsGrounded()
        // {
        //     return _view._transform.position.y <= _groundLevel + _radius + float.Epsilon && _velocity.y <= 0;
        // }
    }
}