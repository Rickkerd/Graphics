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
        public Vector3 up, realup, right, screencenter, P0, P1, P2;

            
        public Camera(Vector3 position2, Vector3 direction2, float distance2)
        {
            position = position2;
            direction = direction2;
            distance = distance2;
            up = new Vector3(0, direction.Z, -direction.Y).Normalized();//Picks a basic normalised up-vector
            right = Vector3.Cross(direction, up).Normalized();//Calculates the vector to the right
            up = Vector3.Cross(direction, right).Normalized();//Calculate a new, better up-vector
            realup = Vector3.Cross(right, up);//Calculate the up-vector, so it is perpendicular to the other axis, thus creating a 
            screencenter = position + distance * direction;
            P0 = (screencenter + realup - right).Normalized(); //(-1, -1, 0)
            P1 = (screencenter + realup + right).Normalized(); //(1, -1, 0)
            P2 = (screencenter - realup - right).Normalized(); //(-1, 1, 0)
        }
    }
}
