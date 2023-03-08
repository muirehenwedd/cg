namespace Primitives3D.Abstractions;

public interface IScene
{
    float[,] Render();
    void AddRayIntersectableObject(IRayIntersectable obj);
}