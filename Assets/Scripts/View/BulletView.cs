using UnityEngine;


namespace PlatformerMvc
{
    public class BulletView: LevelObjectView
    {
        [SerializeField]
        private TrailRenderer _trail;

        public void SetVisible(bool visible)
        {
            if (_trail) _trail.enabled = visible;
            if (_trail) _trail.Clear();
            _spriteRenderer.enabled = visible;
        }
    }
}