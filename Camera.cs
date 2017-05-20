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

        Vector3 Screencenter;
            
        public Camera(Vector3 position, Vector3 direction, float distance)
        {
            Vector3 screencenter = position + distance * direction;
            Vector3 P0 = screencenter + new Vector3(-1, -1, 0);
            Vector3 P1 = screencenter + new Vector3(1, -1, 0);
            Vector3 P2 = screencenter + new Vector3(-1, 1, 0);
        }
    }
}
