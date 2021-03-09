using System;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMvc
{
    public class SpriteAnimatorController : IUpdate, IDisposable
    {
        private sealed class Animator : IUpdate
        {
            public AnimState Track;
            public List<Sprite> Sprites;
            public bool Loop = true;
            public float Speed = 10;
            public float Counter = 0;
            public bool Sleeps;

            public void Update()
            {
                if (Sleeps) return;
                Counter += Time.deltaTime * Speed;

                if (Loop)
                {
                    while (Counter > Sprites.Count)
                    {
                        Counter -= Sprites.Count;
                    }
                }
                else if (Counter > Sprites.Count)
                {
                    Counter = Sprites.Count;
                    Sleeps = true;
                }
            }
        }

        private SpriteAnimatorConfig _config;
        private Dictionary<SpriteRenderer, Animator> _activeAnimation = new Dictionary<SpriteRenderer, Animator>();

        public SpriteAnimatorController(SpriteAnimatorConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool loop, float speed)
        {
            if (_activeAnimation.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Sleeps = false;
                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites;
                    animation.Counter = 0;
                }
            }
            else
            {
                _activeAnimation.Add(spriteRenderer, new Animator()
                {
                    Track = track,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites,
                    Loop = loop,
                    Speed = speed
                });
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimation.ContainsKey(sprite))
            {
                _activeAnimation.Remove(sprite);
            }
        }
        
        public void Update()
        {
            foreach (var animation in _activeAnimation)
            {
                animation.Value.Update();
                if (animation.Value.Counter < animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int) animation.Value.Counter];
                }
            }
        }

        public void Dispose()
        {
            _activeAnimation.Clear();
        }
    }
}