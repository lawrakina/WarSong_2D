using System;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMvc
{
    [CreateAssetMenu(fileName = "BackgroundCfg", menuName = "Configs/BackgroundCfg", order = 2)]
    public sealed class BackgroundData : ScriptableObject
    {
        [Serializable]
        public class BackGroundLayer
        {
            public List<Transform> Layers = new List<Transform>();
        }

        public BackGroundLayer _groundLayers;
        public Vector3 _offsetBackground;
        public Vector3 _cameraStartPosition;
        public float _coeficientMovingBackground = 1.4f;
    }
}