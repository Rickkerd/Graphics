using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    class Scene
    {
        public List<Light> listLight;
        public List<Primitive> listPrimitive;
        public Scene()
        {
            listLight = new List<Light>();
            listPrimitive = new List<Primitive>();
        }
        public Intersection intersectScene(Ray ray)
        {
            float nearestDistance = float.PositiveInfinity;
            Intersection nearestIntersection = null;

            foreach (Primitive p in listPrimitive)
            {
                Intersection currIntersection = null;
                if (p is Sphere)
                {
                    Sphere sphere = (Sphere)p;
                    currIntersection = sphere.IntersectSphere(ray);
                }

                if (p is Plane)
                {
                    Plane plane = (Plane)p;
                    currIntersection = plane.IntersectPlane(ray);
                }
                if (currIntersection != null && currIntersection.distance < nearestDistance)
                {
                    nearestIntersection = currIntersection;
                    nearestDistance = currIntersection.distance;
                }
            }
            return nearestIntersection;
        }
    }
}
