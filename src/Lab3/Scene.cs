using Primitives3D;
using Primitives3D.Abstractions;

namespace Lab3;

public sealed class Scene : IScene
{
    private ICamera? Camera { get; set; }
    private ILightSource? LightSource { get; set; }
    private List<IRayIntersectable> _rayIntersectables = new();

    public void SetCamera(ICamera camera)
    {
        Camera = camera;
    }

    public void AddRayIntersectableObject(IRayIntersectable obj)
    {
        _rayIntersectables.Add(obj);
    }

    public void AddRayIntersectableObjects<T>(IEnumerable<T> objects) where T : IRayIntersectable
    {
        _rayIntersectables.AddRange(objects.Cast<IRayIntersectable>());
    }

    public void SetLightSource(ILightSource lightSource)
    {
        LightSource = lightSource;
    }

    public float[,] Render()
    {
        if (Camera is null)
            throw new NullReferenceException("Camera is not set.");

        if (LightSource is null)
            throw new NullReferenceException("Light source is not set");

        var centersOfPixels = Camera.GetCentersOfPixels();

        var renderResult = new float[centersOfPixels.GetLength(0), centersOfPixels.GetLength(1)];

        Parallel.For(0, centersOfPixels.GetLength(0), i =>
        {
            for (var j = 0; j < centersOfPixels.GetLength(1); j++)
            {
                var pixelCenter = centersOfPixels[i, j];
                var traceRay = new Ray(Camera.ViewPoint, pixelCenter);

                var minimalDistance = float.MaxValue;
                Plane? closestPlane = null;

                for (var k = 0; k < _rayIntersectables.Count; k++)
                {
                    var intersection = _rayIntersectables[k].CalculateIntersectionsPlane(traceRay);

                    if (intersection is null)
                        continue;

                    var distance = (Camera.ViewPoint - intersection.Value.Normal.Point).Length;

                    if (!(distance < minimalDistance)) continue;

                    minimalDistance = distance;
                    closestPlane = intersection;
                }

                renderResult[i, j] = closestPlane is null
                    ? 0
                    : Vector.DotProduct(closestPlane.Value.Normal.Direction, LightSource.Direction) *
                      (IsInShadow(closestPlane.Value.Normal.Point) ? 0.3f : 1);
            }
        });

        return renderResult;
    }

    private bool IsInShadow(Point point)
    {
        var traceRay = new Ray(point, LightSource!.Direction);

        for (var i = 0; i < _rayIntersectables.Count; i++)
        {
            var intersectionPoint = _rayIntersectables[i].CalculateIntersectionsPoint(traceRay);

            if (intersectionPoint.HasValue && !point.IsApproximatelyEqual(intersectionPoint.Value, 1e-5f))
                return true;
        }

        return false;
    }
}