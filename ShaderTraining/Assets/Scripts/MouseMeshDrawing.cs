using UnityEngine;

public class MouseMeshDrawing : MonoBehaviour
{
    MeshFilter _meshFilter;
    MeshRenderer _meshRenderer;
    Mesh _mesh;
    Vector3[] _vertices;
    int[] _triangles;
    int _vertexIndex;

    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;
        _meshRenderer.material = new Material(Shader.Find("Standard"));

        _vertices = new Vector3[0];
        _triangles = new int[0];
        _vertexIndex = 0;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            AddVertex(worldPosition);
            UpdateMesh();
        }
    }

    void AddVertex(Vector3 vertex)
    {
        if (_vertices.Length == _vertexIndex)
            System.Array.Resize(ref _vertices, _vertexIndex + 1);
        
        _vertices[_vertexIndex] = vertex;
        _vertexIndex++;
    }

    void UpdateMesh()
    {
        int numVertices = _vertexIndex;
        int numTriangles = numVertices - 2;

        if (_triangles.Length != numTriangles * 3)
            System.Array.Resize(ref _triangles, numTriangles * 3);

        for (int i = 0; i < numTriangles; i++)
        {
            _triangles[i * 3] = 0;
            _triangles[i * 3 + 1] = i + 1;
            _triangles[i * 3 + 2] = i + 2;
        }

        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
    }
}