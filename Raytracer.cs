using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Raytracer
    {
        Scene scene;
        public Surface screen;
        public void Render()
        {
            Camera c = new Camera(new Vector3(0, 0, 0), new Vector3(0, 0, 1), 1);
            for (float x = 0; x < 512; x++)
                for (float y = 0; y < 512; y++)
                {
                    Ray ray = new Ray();
                    ray.origin = c.position;
                    ray.direction = (new Vector3(-1 + (x * 2 / 512), -1 + (y * 2 / 512), 1)).Normalized();
                    //ray.direction = Vector3.UnitZ;
                    screen.pixels[(int)x + (int)y * screen.width] = CreateColor(TraceRay(ray));
                }            
        }

        public Raytracer()
        {
            scene = new Scene();
            Sphere sphere1 = new Sphere();
            sphere1.position = new Vector3(0, 0, 20);
            sphere1.r = 4;
            sphere1.color = new Vector3(1, 1, 0);
            Plane plane1 = new Plane();
            plane1.direction = new Vector3(0, 1, 0);
            plane1.distance = 1;
            plane1.color = new Vector3(0, 0, 1);
            scene.listPrimitive.Add(sphere1);
            //scene.listPrimitive.Add(plane1);
        }

        Vector3 TraceRay(Ray ray)
        {
            Intersection intersect = scene.intersectScene(ray);
            if (intersect == null)
            {
                return Vector3.Zero;
            }
            else
            {
                return intersect.collider.color;
            }
        }

        int CreateColor(Vector3 color)
        {
            int red = (int)(color.X * 255);
            int green = (int)(color.Y * 255);
            int blue = (int)(color.Z * 255);

            red = Math.Min(255, red);
            green = Math.Min(255, green);
            blue = Math.Min(255, blue);
            return (red << 16) + (green << 8) + blue;
        }
    }
}
