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
    public Vector3 force;
    int eventIndexer = 0;

    public float srcx2 = 0f;
    public float srcy2 = 0f;
    public GameObject mother_blob222;

    private Vector3[] baseHeight;
    Mesh mesh;
    public List<float> eventTimes;
    public List<Vector3> eventPlaces;

    [Header("Plane sizes")]
    public int width = 1;
    public int height = 1;
    public float xSize, ySize = 1;
    public float eventTime;
    
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
            eventTimes.Add(Time.time);
            Vector3 a;// = new Vector3();// eventPlaces[eventIndexer];
            a.x = srcx1;
            a.y = srcy1;
            a.z = 0;
            eventPlaces.Add(a);
            if (eventPlaces.Count>25)
            eventPlaces.RemoveAt(0);
        }
        Vector3 z = new Vector3(mother_blob222.transform.position.x, mother_blob222.transform.position.y, mother_blob222.transform.position.z);
        force = waveForce(z);
        mother_blob222.transform.position = mother_blob222.transform.position + 1000 * force * Time.deltaTime;
        //print(force);
     
    }
   public Vector3 waveForce(Vector3 position)
    {
        for (int i= 0;i< eventPlaces.Count; i++) {
            //Vector3 z = new Vector3(mother_blob2.position);
            eventPos = eventPlaces[i];
            eventTime = eventTimes[i];

            Vector3 force; //= position;
        float distance1 = Mathf.Sqrt(Mathf.Pow(position.x - eventPos.x, 2) + Mathf.Pow(position.z - eventPos.y, 2));
        float wavecentre = Mathf.Abs(((Time.time - eventTime) * speed) - distance1);
        float forceAmplitude = Mathf.Cos(wavecentre * 3.14f) * (((wavecentre < 1) ? (1f - wavecentre) : 0) - 0) * waveSize;
        force.x = position.x - eventPos.x;
        force.y = 0;
        force.z = position.z - eventPos.y;
        force = Vector3.Normalize(force);
        force = force * forceAmplitude;
        }

        return force;
    }
}

