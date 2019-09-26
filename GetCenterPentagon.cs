using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCenterPentagon : MonoBehaviour
{
    public Mesh mesh;
    public Vector3[] vertices;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] = vertices[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
