using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMvc
{
    public class CannonView : MonoBehaviour
    {
        public Transform _muzzleTransform;
        public Transform _emitterTransform;
        public List<LevelObjectView> _bullets;
    }
}