using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMvc
{
    public class BulletsEmitterController : IUpdate
    {
        private List<BulletController> _bullets = new List<BulletController>();
        private Transform _transform;

        private int _currentIndex;
        private float _timeTillNextBullet ;

        private const float _delay = 2;
        private const float _startSpeed = 9;

        public BulletsEmitterController(List<LevelObjectView> bulletViews, Transform transform)
        {
            _transform = transform;

            foreach (LevelObjectView bulletView in bulletViews)
            {
                _bullets.Add(new BulletController(bulletView));
            }
        }

        public void Update()
        {
            if (_timeTillNextBullet > 0)
            {
                _bullets[_currentIndex].Active(false);
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                _timeTillNextBullet = _delay;

                _bullets[_currentIndex].Throw(_transform.position, -_transform.up * _startSpeed);
                _currentIndex++;

                if (_currentIndex >= _bullets.Count)
                {
                    _currentIndex = 0;
                }
            }

            // _bullets.ForEach(b => b.Update());
        }
    }
}