using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    class Intersection
    {
        public Vector3 intersectionPoint;
        public Vector3 intersectionNormal;
        public Primitive collider;
        public float distance;
        public Vector3 MPvec;
        public bool isMirror;
        public bool isSemiMirror;
    }
}
