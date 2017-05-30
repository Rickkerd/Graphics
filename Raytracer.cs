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
            Camera c = new Camera(new Vector3(app.moveX, app.moveY, app.moveZ), new Vector3(0, -1, 0), 1);
            for (float x = 0; x < 512; x++)
                for (float y = 0; y < 512; y++)
                {
                    Ray ray = new Ray();
                    ray.origin = c.position;
                    ray.direction = (new Vector3(c.P1.X - c.P0.X + (x * 2 / 512), c.P0.Y  - c.P2.Y + (y * 2 / 512), c.distance)).Normalized();
                    screen.pixels[(int)x + (int)y * screen.width] = CreateColor(TraceRay(ray.origin, ray));
                }            
        }

        public Raytracer()
        {
            scene = new Scene();
            Sphere sphere1 = new Sphere();
            sphere1.position = new Vector3(-3, 0, 5);
            sphere1.r = 1f;
            sphere1.color = new Vector3(1, 0, 0);
            sphere1.isSemiMirror = true;
            Sphere sphere2 = new Sphere();
            sphere2.position = new Vector3(0, 0, 5);
            sphere2.r = 1;
            sphere2.color = new Vector3(0, 0, 0.6f);
            Sphere sphere3 = new Sphere();
            sphere3.position = new Vector3(3, 0, 5);
            sphere3.r = 1;
            sphere3.color = new Vector3(1f, 1f, 0.5f);
            sphere3.isMirror = true;
            Plane plane1 = new Plane();
            plane1.direction = new Vector3(0, -1, 0);
            plane1.distance = 1f;
            plane1.color = new Vector3(0.2f, 0.2f, 0);
            Plane plane2 = new Plane();
            plane2.direction = new Vector3(0, 0, -1);
            plane2.distance = 10;
            plane2.color = new Vector3(0.2f, 0.5f, 1);
            Plane plane3 = new Plane();
            plane3.direction = new Vector3(0, 0, -1);
            plane3.distance = -5;
            plane3.color = new Vector3(0.03f, 0.05f, 0.03f);
            Plane plane4 = new Plane();
            plane4.direction = new Vector3(-1, 0, 0);
            plane4.distance = 10;
            plane4.color = new Vector3(1, 0, 1);
            Plane plane5 = new Plane();
            plane5.direction = new Vector3(1, 0, 0);
            plane5.distance = 10;
            plane5.color = new Vector3(0.3f, 0.5f, 1);
            Light light1 = new Light();
            light1.position = new Vector3(0 , -2, -5);
            light1.brightness = new Vector3(0.6f, 0.6f, 0.6f);
            scene.listPrimitive.Add(sphere1);
            scene.listPrimitive.Add(sphere2);
            scene.listPrimitive.Add(sphere3);
            scene.listPrimitive.Add(plane1);
            scene.listPrimitive.Add(plane2);
            scene.listPrimitive.Add(plane3);
            scene.listPrimitive.Add(plane4);
            scene.listPrimitive.Add(plane5);
            scene.listLight.Add(light1);
        }

        Vector3 TraceRay(Vector3 Origin, Ray ray, int parameter = 0)
        {
            Intersection intersect = scene.intersectScene(ray);
            if (intersect == null || parameter > 2)
            {
                return Vector3.Zero;
            }
            if (intersect.isMirror)
            {
                return TraceRay(intersect.intersectionPoint, Reflect(intersect, ray), parameter+1);
            }
            else if (intersect.isSemiMirror)
            {
                return ((TraceRay(intersect.intersectionPoint, Reflect(intersect, ray), parameter + 1)) + (DirectIllumination(intersect) * intersect.collider.color * 255)) / 2;
            }
            else
            {
                return DirectIllumination(intersect) * intersect.collider.color * 255;
            }
        }

        Ray Reflect(Intersection i, Ray ray)
        {
            Ray newRay = new Ray();
            newRay.direction = (ray.direction - 2 * i.intersectionNormal * Vector3.Dot(ray.direction, i.intersectionNormal));
            newRay.origin = (i.intersectionPoint + 0.02f * newRay.direction).Normalized();
            return newRay;
        }

        Vector3 DirectIllumination(Intersection i)
        {
            Vector3 q = new Vector3();
            foreach (Light l in scene.listLight)
            {
                Vector3 L = (l.position - i.intersectionPoint);
                float dist = L.Length;
                L = L.Normalized();
                if (!IsVisible(i.intersectionNormal, L))
                    return Vector3.Zero;
                float attenuation = 1 / (dist * dist);
                q = MathHelper.Clamp(Vector3.Dot(i.intersectionNormal, L), 0, 1) * attenuation * l.brightness;
           }
           return q;
        }

        bool IsVisible(Vector3 N, Vector3 L)
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
