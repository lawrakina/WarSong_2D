using PlatformerMvc.Utils;
using TMPro;
using UnityEngine;

namespace PlatformerMvc
{
    public class PlayerMove: IUpdate
    {
        private const float _walkSpeed = 3f;
        private const float _animationSpeed = 10f;
        private const float _jumpStartSpeed = 8f;
        private const float _movingThresh = 0.1f;
        private const float _flyThresh = 3f;
        private const float _groundLevel = 0.5f;
        private const float _g = -9.81f;
        
        private Vector3 _leftScale = new Vector3(-1,1,1);
        private Vector3 _rightScale = new Vector3(1,1,1);
        
        private float _yVelocity = 0;
        private bool _doJump;
        private float _xAxisInput;

        private LevelObjectView _view;
        private SpriteAnimator _spriteAnimator;

        public PlayerMove(LevelObjectView view, SpriteAnimator spriteAnimator)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
        }

        private void GoSideWay()
        {
            _view._transform.position += Vector3.right * (Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1));
            _view._transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        public bool IsGrounded()
        {
            return _view._transform.position.y <= _groundLevel + float.Epsilon && _yVelocity <= 0;
        }

        public void Update()
        {
            _doJump = Input.GetAxis("Vertical") > 0;
            _xAxisInput = Input.GetAxis("Horizontal");
            var goSideWay = Mathf.Abs(_xAxisInput) > _movingThresh;

            if (IsGrounded())
            {
                //walking
                if(goSideWay) GoSideWay();
                _spriteAnimator.StartAnimation(_view._spriteRenderer, goSideWay ? AnimState.Run : AnimState.Idle, true, _animationSpeed);
                
                //start jump
                if (_doJump && _yVelocity == 0)
                {
                    _yVelocity = _jumpStartSpeed;
                }
                //stop jump
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _view._transform.position = _view._transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                //flying
                if(goSideWay) GoSideWay();
                if (Mathf.Abs(_yVelocity) > _flyThresh)
                {
                    _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimState.Jump, false, _animationSpeed);
                }
                _yVelocity += _g * Time.deltaTime;
                _view._transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }
            
        }
    }
}