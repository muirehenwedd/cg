using Lab1;
using Lab1.Output;
using Primitives3D;

var scene = new SimpleScene();

var lightSource = new SimpleLightSource(new Vector(-1, 0, 0));
scene.SetLightSource(lightSource);

var sphere1 = new Sphere(new Point(15, -5, -5), 3);
scene.AddRayIntersectableObject(sphere1);

var sphere2 = new Sphere(new Point(15, 5, -5), 3);
scene.AddRayIntersectableObject(sphere2);

var sphere3 = new Sphere(new Point(15, 5, 5), 3);
scene.AddRayIntersectableObject(sphere3);

var sphere4 = new Sphere(new Point(15, -5, 5), 3);
scene.AddRayIntersectableObject(sphere4);

var camera = new SimpleCamera(5, 45, 80);
scene.SetCamera(camera);

var output = new ConsoleShadesImageOutput();
output.Output(scene.Render());