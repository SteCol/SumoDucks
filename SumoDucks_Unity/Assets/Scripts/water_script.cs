using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_script : MonoBehaviour {

    float scale = 0.3f;
    float speed = 5.0f;
    float noiseStrength = 1f;
    float noiseWalk = 1f;
    public float srcx1 = 0f;
    public float srcy1 = 0f;

    public float srcx2 = 0f;
    public float srcy2 = 0f;

    private Vector3[] baseHeight;
    Mesh mesh;

    [Header("Plane sizes")]
    public int width = 1;
    public int height = 1;
    public float xSize, ySize = 1;
    public float eventTime;
    public float eventTimes[];
    public float waveSize = 1f;

    Vector3 eventPos;
    // Use this for initialization
    void Awake () {
        mesh = GetComponent<MeshFilter>().mesh = ParametricPlane.GeneratePlane(width, height, xSize / 2, ySize / 2, 0, xSize, ySize);
        waveSize = 1f;

}

// Update is called once per frame
void Update () {
        
        if (baseHeight == null)
            baseHeight = mesh.vertices;

        Vector3[] vertices = new Vector3[baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            //vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * scale;
            float distance1 = Mathf.Sqrt(Mathf.Pow(baseHeight[i].x - eventPos.x, 2) + Mathf.Pow(baseHeight[i].z - eventPos.y, 2));
            //   vertex.y += (Mathf.Sin(Time.time * -speed + 2f * distance1 + baseHeight[i].y * 0 ) * scale) *1 / (distance1 + 1f);

            //vertex.y += (Mathf.Sin((Time.time-eventTime) * -speed + 2f * (distance1- (Time.time - eventTime)) + baseHeight[i].y * 0) * scale) * 1 / (distance1 + 1f);
            float wavecentre = Mathf.Abs(((Time.time - eventTime) * speed) - distance1);
            vertex.y = Mathf.Cos(wavecentre*3.14f)*(((wavecentre < 1 ) ? (1f-wavecentre): 0)- 0) * waveSize ;

            float distance2 = Mathf.Sqrt(Mathf.Pow(baseHeight[i].x - srcx2, 2) + Mathf.Pow(baseHeight[i].z - srcy2, 2));
          //  vertex.y += Mathf.Sin(Time.time * -speed + 3f*distance2 + baseHeight[i].y * 0) * scale *1/ (distance2 + 1f);

            //vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        if (Input.GetKeyDown(KeyCode.K))
        {
            print("k has been pressed");
            eventTime = Time.time;
            eventPos.x = srcx1;
            eventPos.y = srcy1;

        }
    }
    }

