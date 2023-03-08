using Lab1;
using Primitives3D;

var scene = new SimpleScene();

var lightSource = new SimpleLightSource(new Vector(-3, 0, 1));
scene.SetLightSource(lightSource);

var sphere1 = new Sphere(new Point(15, 0, 0), 10);
scene.AddRayIntersectableObject(sphere1);

//var sphere2 = new Sphere(new Point(15, 5, 10), 7);
//scene.SetSphere(sphere2);

var camera = new SimpleCamera() {PixelSize = 1};
scene.SetCamera(camera);

ConsoleOutput.ShadesDisplay(scene.Render());