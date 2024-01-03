using System;
using UnityEngine;

namespace Utils
{
    public static class RaycastHelper
    {
        public static RaycastHit[] RaycastObjects(Ray ray, 
            float distance, 
            int hitCount)
        {
            RaycastHit[] results = new RaycastHit[hitCount];
            Physics.RaycastNonAlloc(ray, results, distance);
            return results;
        }

        public static RaycastHit[] RaycastObjects(Func<Ray> rayCreationFunc, 
            float distance, 
            int hitCount)
        {
            Ray ray = rayCreationFunc();
            RaycastHit[] results = new RaycastHit[hitCount];
            Physics.RaycastNonAlloc(ray, results, distance);
            return results;
        }

        public static Vector3 ClosestPointPlaneFacedToCamera(Camera camera, Vector3 origin, Ray cameraRay)
        {
            var movePlane = new Plane(camera.transform.forward, origin);

            movePlane.Raycast(cameraRay, out float distance);
            
            return cameraRay.GetPoint(distance);
        }
        public static T GetClosest<T>(ref RaycastHit[] hits) where T : MonoBehaviour
        {
            float minSelectedDistance = float.MaxValue;
            T current = null;
            
            for(int i = 0; i < hits.Length; i++)
            {
                if(hits[i].collider == null)
                    continue;
                
                if (hits[i].collider.gameObject.TryGetComponent(out T selectObject))
                {
                    if (hits[i].distance < minSelectedDistance)
                    {
                        minSelectedDistance = hits[i].distance;
                        current = selectObject;
                    }
                }

            }
            return current;
        }
    }
}