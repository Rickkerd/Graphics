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
        Camera c;
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
            sphere1.position = new Vector3(-6, -5, 10);
            sphere1.r = 2;
            sphere1.color = new Vector3(1, 1, 0);
            Sphere sphere2 = new Sphere();
            sphere2.position = new Vector3(1, -1, 6);
            sphere2.r = 1;
            sphere2.color = new Vector3(1, 1, 1);
            Plane plane1 = new Plane();
            plane1.direction = new Vector3(0, 0, 1);
            plane1.distance = 20;
            plane1.color = new Vector3(0, 0, 1);
            Light light1 = new Light();
            light1.position = new Vector3(-6, -5, 5);
            scene.listPrimitive.Add(sphere1);
            scene.listPrimitive.Add(sphere2);
            //scene.listPrimitive.Add(plane1);
            scene.listLight.Add(light1);
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
                //return intersect.collider.color;
                return DirectIllumination(intersect, ray) * 0.1f;
            }
        }

        Vector3 DirectIllumination(Intersection i, Ray ray)
        {
            Vector3 q;
            Light l = new Light();
            //foreach (Light l in scene.listLight)
            //{
                Vector3 L = (l.position - i.intersectionPoint);
                //if...
                float dist = (float)Math.Sqrt(L.X * L.X + L.Y + L.Y + L.Z + L.Z);
                float attenuation = 1 / (dist * dist);
                L = L.Normalized();
                q = new Vector3(1f, 1f, 1f) * Vector3.Dot(ray.direction, L) * attenuation;
           //}
            return q;
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
