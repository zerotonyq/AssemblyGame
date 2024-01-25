using UnityEngine;

namespace Utils
{
    public static class DistanceChecker
    {
        public static Transform GetClosest(Vector3 point, Transform target1, Transform target2)
        {
            var distanceVector1 = point - target1.position;
            var distanceVector2 = point - target2.position;

            if (distanceVector1.magnitude >= distanceVector2.magnitude)
                return target1;
            else
                return target2;
        }
        
    }
}