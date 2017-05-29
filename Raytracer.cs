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
        Application app = new Application();
        public Surface screen;
        public int recursion = 0;
        public void Render()
        {
            app.Control();
            Camera c = new Camera(new Vector3(app.move.X, app.move.Y, app.move.Z), new Vector3(0, 0, 1), 1);
            for (float x = 0; x < 512; x++)
                for (float y = 0; y < 512; y++)
                {
                    Ray ray = new Ray();
                    ray.origin = c.position;
                    ray.direction = (new Vector3(c.P1.X - c.P0.X + (x * 2 / 512), c.P0.Y  - c.P2.Y + (y * 2 / 512), c.distance)).Normalized();
                    //ray.direction = Vector3.UnitZ;
                    screen.pixels[(int)x + (int)y * screen.width] = CreateColor(TraceRay(ray.origin, ray));
                }            
        }

        public Raytracer()
        {
            scene = new Scene();
            Sphere sphere1 = new Sphere();
            sphere1.position = new Vector3(0, -2, 8);
            sphere1.r = 2;
            sphere1.color = new Vector3(1, 0, 0);
            sphere1.isMirror = true;
            Sphere sphere2 = new Sphere();
            sphere2.position = new Vector3(-4, -1, 7);
            sphere2.r = 1;
            sphere2.color = new Vector3(1, 1, 1);
            sphere2.isMirror = false;
            Sphere sphere3 = new Sphere();
            sphere3.position = new Vector3(-2, -8, 30);
            sphere3.r = 7;
            sphere3.color = new Vector3(1f, 0.2f, 0.5f);
            sphere3.isMirror = false;
            Sphere sphere4 = new Sphere();
            sphere4.position = new Vector3(3, -1, 7);
            sphere4.r = 1;
            sphere4.color = new Vector3(0.5f, 1, 0.1f);
            sphere4.isMirror = false;
            Plane plane1 = new Plane();
            plane1.direction = new Vector3(0, -1, 0);
            plane1.distance = 0.1f;
            plane1.color = new Vector3(0, 0, 0.2f);
            Light light1 = new Light();
            light1.position = new Vector3(0 , -2, 1);
            light1.brightness = new Vector3(0.2f, 0.5f, 1f);
            Light light2 = new Light();
            light2.position = new Vector3(0, -2, 10);
            light2.brightness = new Vector3(0.5f, 0.5f, 0.5f);
            scene.listPrimitive.Add(sphere1);
            scene.listPrimitive.Add(sphere2);
            scene.listPrimitive.Add(sphere3);
            scene.listPrimitive.Add(sphere4);
            scene.listPrimitive.Add(plane1);
            scene.listLight.Add(light1);
            //scene.listLight.Add(light2);
        }

        Vector3 TraceRay(Vector3 Origin, Ray ray)
        {
            Intersection intersect = scene.intersectScene(ray);
            if (intersect == null)
            {
                return Vector3.Zero;
            }
            else
            {
                Vector3 N = 1 * new Vector3(intersect.intersectionPoint - intersect.MPvec).Normalized();
                if (intersect.isMirror && recursion < 3)
                {
                    recursion++;
                    //ray.origin = intersect.intersectionPoint;
                    return TraceRay(intersect.intersectionPoint, Reflect(intersect, N));
                }
               /* else if (intersect.isDielectric && recursion < 3)
                {
                    recursion++;
                }*/
                else
                {
                    recursion = 0;
                    //return intersect.collider.color;
                    return DirectIllumination(intersect, N) * intersect.collider.color * 255;
                }
            }
        }

        Ray Reflect(Intersection i, Vector3 N)
        {
            double a = Math.Acos(Vector3.Dot(i.intersectionPoint.Normalized(), N.Normalized()));
            //double a = Math.Acos(ray.direction.Length / N.Length);
            Ray newRay = new Ray();
            newRay.origin = i.intersectionPoint * 1.01f;
            newRay.direction = new Vector3((float)(i.intersectionPoint.Normalized().Length * N.Normalized().Length * Math.Cos(a)) / N.Normalized().X, (float)(i.intersectionPoint.Normalized().Length * N.Normalized().Length * Math.Cos(a)) / N.Normalized().Y, (float)(i.intersectionPoint.Normalized().Length * N.Normalized().Length * Math.Cos(a)) / N.Normalized().Z).Normalized();
            return newRay;
        }

        Vector3 DirectIllumination(Intersection i, Vector3 N)
        {
            Vector3 q = new Vector3();
            foreach (Light l in scene.listLight)
            {
                Vector3 L = (l.position - i.intersectionPoint);
                float dist = L.Length;
                L = L.Normalized();
                if (!IsVisible(N, L, dist))
                    return Vector3.Zero;
                float attenuation = 1 / (dist * dist);
                q = MathHelper.Clamp(Vector3.Dot(N, L), 0, 1) * attenuation * l.brightness;
           }
           return q;
        }

        bool IsVisible(Vector3 N, Vector3 L, float dist)
        {
            if (Vector3.Dot(N, L) < 0)
                return false;
            else return true;
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
