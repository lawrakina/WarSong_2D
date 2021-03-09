using System.Collections.Generic;
using Interface;
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
        private CannonView _cannon;

        [SerializeField]
        private int _animationSpeed = 10;

        [SerializeField]
        private List<LevelObjectView> _coinsList;

        private SpriteAnimatorController _playerAnimatorController;
        private SpriteAnimatorController _coinAnimatorController;
        private PalaraxManager _palaraxManager;

        private List<IInit> _inits;
        private List<IUpdate> _updaters;
        private List<IFixedUpdate> _fixedUpdaters;
        private MainHeroPhysicsWalker _playerController;
        private CannonAimController _cannonAim;
        private BulletsEmitterController _bulletEmitterController;
        private CoinsManager _coinsManager;
        
        private void Awake()
        {
            _inits = new List<IInit>();
            _updaters = new List<IUpdate>();
            _fixedUpdaters = new List<IFixedUpdate>();

            var backgroundData = Resources.Load<BackgroundData>("BackgroundCfg");
            _palaraxManager = new PalaraxManager(backgroundData, _camera.transform);

            var playerConfig = Resources.Load<SpriteAnimatorConfig>("AnimPlayerCfg");
            _playerAnimatorController = new SpriteAnimatorController(playerConfig);
            _playerAnimatorController.StartAnimation(_playerView._spriteRenderer, AnimState.Run, true, _animationSpeed);
            _playerController = new MainHeroPhysicsWalker(_playerView, _playerAnimatorController);

            _cannonAim = new CannonAimController(_cannon._muzzleTransform, _playerView.transform);
            _bulletEmitterController = new BulletsEmitterController(_cannon._bullets, _cannon._emitterTransform);

            var cameraController = new CameraController(Camera.main, _playerView._transform);

            var coinConfig = Resources.Load<SpriteAnimatorConfig>("CoinConfig");
            _coinAnimatorController = new SpriteAnimatorController(coinConfig);
            
            _coinsManager = new CoinsManager(_playerView, _coinsList, _coinAnimatorController);
            
            
            _inits.Add(_palaraxManager);
            _updaters.Add(_palaraxManager);
            _updaters.Add(_playerAnimatorController);
            _updaters.Add(_coinAnimatorController);
            _updaters.Add(cameraController);
            _updaters.Add(_cannonAim);
            _updaters.Add(_bulletEmitterController);
            _fixedUpdaters.Add(_playerController);
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
            foreach (var updater in _fixedUpdaters)
            {
                updater.FixedUpdate();
            }
        }
    }
}