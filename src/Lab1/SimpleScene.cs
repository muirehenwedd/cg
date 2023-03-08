using Primitives3D;
using Primitives3D.Abstractions;

namespace Lab1;

public sealed class SimpleScene : IScene
{
    private ICamera? _camera { get; set; }
    private ILightSource? _lightSource { get; set; }
    private List<IRayIntersectable> _rayIntersectables = new();

    public void SetCamera(ICamera camera)
    {
        _camera = camera;
    }

    public void AddRayIntersectableObject(IRayIntersectable obj)
    {
        _rayIntersectables.Add(obj);
    }

    public void SetLightSource(SimpleLightSource simpleLightSource)
    {
        _lightSource = simpleLightSource;
    }

    public float[,] Render()
    {
        if (_camera is null)
            throw new NullReferenceException("Camera is not set.");

        if (_lightSource is null)
            throw new NullReferenceException("Light source is not set");

        var centersOfPixels = _camera.GetCentersOfPixels();

        var renderResult = new float[centersOfPixels.GetLength(0), centersOfPixels.GetLength(1)];

        for (var i = 0; i < centersOfPixels.GetLength(0); i++)
        {
            for (var j = 0; j < centersOfPixels.GetLength(0); j++)
            {
                var pixelCenter = centersOfPixels[i, j];
                var traceRay = new Ray(_camera.ViewPoint, pixelCenter);

                var closestIntersection = _rayIntersectables
                    .SelectMany(o => o.CalculateIntersectionsPlanes(traceRay))
                    .OrderBy(plane => (_camera.ViewPoint - plane.Normal.Point).Length)
                    .ToArray();

                renderResult[i, j] = closestIntersection.Length == 0
                    ? 0
                    : Vector.DotProduct(closestIntersection[0].Normal.Direction, _lightSource.Direction);
            }
        }

        return renderResult;
    }
}