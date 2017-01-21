using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_script : MonoBehaviour {

    float scale = 0.3f;
    float speed = 5.0f;
    float noiseStrength = 1f;
    float noiseWalk = 1f;

    private Vector3[] baseHeight;
    Mesh mesh;

    [Header("Plane sizes")]
    public int width = 1;
    public int height = 1;
    public float xSize, ySize = 1;

    // Use this for initialization
    void Awake () {
        mesh = GetComponent<MeshFilter>().mesh = ParametricPlane.GeneratePlane(width, height, xSize / 2, ySize / 2, 0, xSize, ySize);
    }
	
	// Update is called once per frame
	void Update () {

        if (baseHeight == null)
            baseHeight = mesh.vertices;

        Vector3[] vertices = new Vector3[baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * scale;
            vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
