using System.Globalization;
using Lab3.Abstractions;
using Primitives3D;

namespace Lab3;

public class ObjReader : IObjReader
{
    public Triangle[] ReadObjFile(string path)
    {
        var lines = File.ReadAllLines(path);

        var vertices = new List<Point>();
        var triangles = new List<Triangle>();

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var parts = line.Split(' ');
            var type = parts[0];

            if (type == "v") // Vertex
            {
                var x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                var y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                var z = float.Parse(parts[3], CultureInfo.InvariantCulture);

                vertices.Add(new Point(x, y, z));
            }
            else if (type == "f") // Face
            {
                var indices = parts.Skip(1).Select(i => int.Parse(i.Split('/')[0]) - 1).ToArray();

                if (indices.Length != 3)
                {
                    throw new FormatException("Only triangular faces are supported.");
                }

                triangles.Add(new Triangle(vertices[indices[0]], vertices[indices[1]], vertices[indices[2]]));
            }
        }

        return triangles.ToArray();
    }
}