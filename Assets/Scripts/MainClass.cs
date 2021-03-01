using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMvc
{
    public sealed class MainClass : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        
        
        [SerializeField]
        private LevelObjectView _playerView;

        [SerializeField]
        private int _animationSpeed = 10;

        private SpriteAnimator _playerAnimator;
        private PalaraxManager _palaraxManager;

        private List<IInit> _inits;
        private List<IUpdate> _updaters;
        
        private void Awake()
        {
            _inits = new List<IInit>();
            _updaters = new List<IUpdate>();

            var backgroundData = Resources.Load<BackgroundData>("BackgroundCfg");
            _palaraxManager = new PalaraxManager(backgroundData, _camera.transform);
            _inits.Add(_palaraxManager);
            _updaters.Add(_palaraxManager);

            var playerConfig = Resources.Load<SpriteAnimatorConfig>("AnimPlayerCfg");
            _playerAnimator = new SpriteAnimator(playerConfig);
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Run, true, _animationSpeed);
            _updaters.Add(_playerAnimator);

            Init();
        }

        private void Init()
        {
            foreach (var init in _inits)
            {
                init.Init();
            }
        }

        private void Update()
        {
            foreach (var updater in _updaters)
            {
                updater.Update();
            }
        }

        private void FixedUpdate()
        {
        }
    }
}