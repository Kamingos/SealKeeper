using UnityEngine;

public sealed class ConvexPolygonArea : MonoBehaviour
{
    [SerializeField] private Transform[] _corners; // минимум 3, по порядку обхода

    public Vector3 GetRandomPoint()
    {
        // Веерная триангуляция от вершины 0. Для выпуклого многоугольника это корректно.
        // Треугольник выбирается с вероятностью, пропорциональной его площади.
        float total = 0f;
        for (int i = 1; i < _corners.Length - 1; i++)
            total += TriangleArea(_corners[0].position, _corners[i].position, _corners[i + 1].position);

        float pick = Random.value * total;
        float acc = 0f;

        for (int i = 1; i < _corners.Length - 1; i++)
        {
            Vector3 a = _corners[0].position;
            Vector3 b = _corners[i].position;
            Vector3 c = _corners[i + 1].position;

            acc += TriangleArea(a, b, c);
            if (pick <= acc || i == _corners.Length - 2)
                return RandomInTriangle(a, b, c);
        }
        return transform.position;
    }

    private static float TriangleArea(Vector3 a, Vector3 b, Vector3 c)
        => Mathf.Abs((b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z)) * 0.5f;

    private static Vector3 RandomInTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        float r1 = Mathf.Sqrt(Random.value);
        float r2 = Random.value;
        return (1 - r1) * a + r1 * (1 - r2) * b + r1 * r2 * c;
    }

    private void OnDrawGizmos()
    {
        if (_corners == null || _corners.Length < 3) return;
        Gizmos.color = Color.green;
        for (int i = 0; i < _corners.Length; i++)
        {
            if (_corners[i] == null) continue;
            var next = _corners[(i + 1) % _corners.Length];
            if (next != null) Gizmos.DrawLine(_corners[i].position, next.position);
        }
    }
}
