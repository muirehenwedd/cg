using Primitives3D;

namespace Lab3.Abstractions;

public interface IObjReader
{
    Triangle[] ReadObjFile(string path);
}