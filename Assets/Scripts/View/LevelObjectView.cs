using System;
using UnityEngine;


namespace PlatformerMvc
{
    public class LevelObjectView: MonoBehaviour
    {
        public Transform _transform;
        public SpriteRenderer _spriteRenderer;
        public Rigidbody2D _rigidbody2d;
        public Collider2D _collider2D;
        public TrailRenderer _trail;
        public Action<LevelObjectView> OnLevelObjectContact { get; set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var levelObject = other.gameObject.GetComponent<LevelObjectView>();
            OnLevelObjectContact?.Invoke(levelObject);
        }
    }
}