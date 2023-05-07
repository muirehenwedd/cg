using Lab1;
using Lab3;
using Lab3.Abstractions;
using Lab3.Configuration;
using Lab3.Configuration.Abstractions;
using Primitives3D;
using Writer.Bmp;

IConfigParser configParser = new ConfigParser();
var config = configParser.Parse(args);

IConfigValidator configValidator = new ConfigValidator();
configValidator.Validate(config);

var reader = new ObjReader();
var triangles = reader.ReadObjFile(config.InputFilePath);

ITransformationBuilder transformationBuilder = TransformationBuilder.Create();
var transformation = transformationBuilder
    .RotateAroundZ(-25)
    .Scale(1, 1, 1.5f)
    .Translate(0, 0, 0.1f)
    .BuildTransformation();

transformation.Apply(triangles);

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

using var file = new FileStream(config.OutputFilePath, FileMode.OpenOrCreate);
file.Write(writer.Write(image));