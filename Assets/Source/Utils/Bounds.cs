using UnityEngine;

namespace FlagCapturing.Utils
{
    public class Bounds
    {
        public Vector2 RangeX { get; private set; }
        public Vector2 RangeY { get; private set; }

        public Bounds(Vector2 bound1, Vector2 bound2)
        {
            float lowerX = Mathf.Min(bound1.x, bound2.x),
                  higherX = Mathf.Max(bound1.x, bound2.x),
                  lowerY = Mathf.Min(bound1.y, bound2.y),
                  higherY = Mathf.Max(bound1.y, bound2.y);
            RangeX = new Vector2(lowerX, higherX);
            RangeY = new Vector2(lowerY, higherY);
        }
    }
}
