using UnityEngine;


namespace PlatformerMvc.Utils
{
    public static class UtilsVectors
    {
        public static Vector3 Change(this Vector3 org, object x = null, object y = null, object z = null)
        {
            return new Vector3(
                x == null ? org.x : (float) x, 
                y == null ? org.y : (float) y,
                z == null ? org.z : (float) z);
        }

        public static Vector2 Change( this Vector3 org, float y1, object x = null, object y = null)
        {
            return new Vector3(
                x == null ? org.x : (float) x, 
                y == null ? org.y : (float) y);
        }
    }
}