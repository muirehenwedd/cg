using Lab1;
using Lab3;
using Lab3.Abstractions;
using Primitives3D;
using Writer.Bmp;

var reader = new ObjReader();
var triangles = reader.ReadObjFile("cow.obj");

var scene = new Scene();

var lightSource = new SimpleLightSource(new Vector(0, 1, 0));
scene.SetLightSource(lightSource);

scene.AddRayIntersectableObjects(triangles);

//uncomment to add shadow.
//scene.AddRayIntersectableObject(new Sphere(new Point(0, -5, 0), 0.1f));

var camera = new Camera(new Point(0, -1, 0), new Vector(0, 1, 0), 40, 1000, 1000);
scene.SetCamera(camera);
var resultMatrix = scene.Render();

IGrayscaleMatrixToMediateImageConverter converter = new GrayscaleMatrixToMediateImageConverter();

var image = converter.Convert(resultMatrix);
var writer = new BmpWriter();

using var file = new FileStream("output.bmp", FileMode.OpenOrCreate);
file.Write(writer.Write(image));