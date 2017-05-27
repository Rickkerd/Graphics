using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Camera
    {
        public Vector3 position;
        public Vector3 direction;
        public float distance;
        public Vector3 up, right, screencenter, P0, P1, P2;

        //Vector3 screencenter;
            
        public Camera(Vector3 position2, Vector3 direction2, float distance2)
        {
            position = position2;
            direction = direction2;
            distance = distance2;
            up = new Vector3(0, direction.Z, -direction.Y);
            right = Vector3.Cross(direction, up);
            up = Vector3.Cross(direction, right);
            screencenter = position + distance * direction;
            P0 = (screencenter + up - right).Normalized(); //(-1, -1, 0)
            P1 = (screencenter + up + right).Normalized(); //(1, -1, 0)
            P2 = (screencenter - up - right).Normalized(); //(-1, 1, 0)
        }
    }
}
