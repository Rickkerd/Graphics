using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    class Primitive
    {
        public Vector3 color;
        public bool isMirror;
        public bool isSemiMirror;
    }
    class Sphere:Primitive
    {
        public Vector3 position;
        public float r;
        public Intersection IntersectSphere(Ray ray)
        {
            Vector3 c = position - ray.origin;
            float t = Vector3.Dot(c, ray.direction);
            Vector3 q = (c - t * ray.direction);
            float p2 = Vector3.Dot(q, q);
            if (p2 > r * r )
            {
                return null;
            }
            t -= (float)Math.Sqrt(r * r - p2);
            Intersection i = new Intersection();
            i.intersectionPoint = ray.origin + t * ray.direction;
            i.collider = this;
            i.distance = t;
            i.MPvec = position;
            i.isMirror = isMirror;
            i.isSemiMirror = isSemiMirror;
            i.intersectionNormal = new Vector3(i.intersectionPoint - i.MPvec).Normalized();
            return i;
        }
    }

    class Plane:Primitive
    {
        public Vector3 direction;
        public float distance;
        public Intersection IntersectPlane(Ray ray)
        {
            float t = (Vector3.Dot(direction, ray.origin) - distance) / Vector3.Dot(direction, ray.direction);
            Vector3 intersectionPoint = ray.origin + t * ray.direction;
            if (t <= 0)
            {
                return null;
            }
            Intersection i = new Intersection();
            i.intersectionPoint = intersectionPoint;
            i.collider = this;
            i.distance = t;
            i.intersectionNormal = direction;
            return i;
        }
    }
}
