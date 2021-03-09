using Interface;
using PlatformerMvc.Utils;
using UnityEngine;


namespace PlatformerMvc
{
    public class MainHeroPhysicsWalker : IFixedUpdate
    {
        private const string _verticalAxisName = "Vertical";
        private const string _horizontalAxisName = "Horizontal";
        
        private const float _animationsSpeed = 10;
        private const float _walkSpeed = 150;
        private const float _jumpForse = 350;
        private const float _jumpThresh = 0.1f;
        private const float _flyThresh = 1f;
        private const float _movingThresh = 0.1f;
        
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        
        private bool _doJump;
        private float _goSideWay = 0;
        private float _xAxisInput;
        
        private readonly LevelObjectView _view;
        private readonly SpriteAnimatorController _spriteAnimatorController;
        private readonly ContactsPoller _contactsPoller;
        
        public MainHeroPhysicsWalker(LevelObjectView view, SpriteAnimatorController spriteAnimatorController)
        {
            _view = view;
            _spriteAnimatorController = spriteAnimatorController;
            _contactsPoller = new ContactsPoller(_view._collider2D);
        }
        
        public void FixedUpdate()
        {
            _doJump = Input.GetAxis(_verticalAxisName) > 0;
            _xAxisInput = Input.GetAxis(_horizontalAxisName);
            _contactsPoller.FixedUpdate();
            var walks = Mathf.Abs(_xAxisInput) > _movingThresh;

            var newVelocity = 0f;

            if (walks && (_xAxisInput > 0 || !_contactsPoller.HasLeftContacts) &&
                (_xAxisInput < 0 || !_contactsPoller.HasRightContacts))
            {
                newVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
                _view._transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
            }

            _view._rigidbody2d.velocity = _view._rigidbody2d.velocity.Change(x: newVelocity);

            if (_contactsPoller.IsGrounded && _doJump && Mathf.Abs(_view._rigidbody2d.velocity.y) <= _jumpThresh)
            {
                _view._rigidbody2d.AddForce(Vector2.up * _jumpForse, ForceMode2D.Impulse);
            }

            if (_contactsPoller.IsGrounded)
            {
                _spriteAnimatorController.StartAnimation(_view._spriteRenderer, walks ? AnimState.Run : AnimState.Idle, true, _animationsSpeed);
            }
            else if (Mathf.Abs(_view._rigidbody2d.velocity.y) > _flyThresh)
            {
                _spriteAnimatorController.StartAnimation(_view._spriteRenderer, AnimState.Jump, false,_animationsSpeed);
            }
        }
    }
}