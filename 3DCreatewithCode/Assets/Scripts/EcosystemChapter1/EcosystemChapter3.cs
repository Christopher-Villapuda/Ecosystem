using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcosystemChapter3 : MonoBehaviour
{ 
    randomFish2 a;
    List<oscillator> oscilattors = new List<oscillator>();
    void Start()
    {
        a = new randomFish2();

        while (oscilattors.Count < 10)
        {
            oscillator o = new oscillator(new Vector3(2f,2f,2f));
            o.oGameObject.transform.SetParent(a.parent.transform);
            oscilattors.Add(o);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        a.Update();
        a.CheckEdges();

        foreach (oscillator o in oscilattors)
        {
            o.x = Mathf.Sin(o.angle.x) * o.amplitude.x;
            o.y = Mathf.Sin(o.angle.y) * o.amplitude.y;
            o.z = Mathf.Sin(o.angle.z) * o.amplitude.z;
            o.angle += o.velocity;
            o.lineRender.SetPosition(1, o.oGameObject.transform.position);
            o.lineRender.SetPosition(0,a.body.transform.position);
            o.oGameObject.transform.transform.Translate(new Vector3(o.x, o.y, o.z) * Time.deltaTime);

            Vector3 difference = a.body.transform.position - o.oGameObject.transform.position;
            float m = difference.magnitude;
             if (m<=1f)
            {
                o.oGameObject.transform.transform.Translate(new Vector3(o.x, o.y, o.z) * Time.deltaTime);
            }
            else
            {
                //o.angle -= o.velocity;
                o.oGameObject.transform.transform.Translate(new Vector3(o.x, o.y, o.z) * Time.deltaTime);
            }
        }
    }
}


    public class randomFish2
{
    public static Vector3 location, velocity, acceleration;
    private float topSpeed;
    private Vector3 minimumPos, maximumPos;
    public GameObject parent = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    public float mass;
    private float radius;
    public Rigidbody body;
    public randomFish2()
    {
        findWindowLimits();
        body = parent.AddComponent<Rigidbody>();
        body.position = Vector3.zero;
        location = Vector3.zero; 
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        topSpeed = 4F;
        radius = .5f;
        Renderer r = parent.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
        parent.transform.position = body.position;
        parent.transform.localScale = 2 * radius * Vector3.one;
        body.mass = (4f / 3f) * Mathf.PI * radius * radius * radius;
        body.useGravity = false;
        body.isKinematic = true;
        Renderer renderer = parent.GetComponent<Renderer>();
        renderer.material.color = Color.blue;
    }

    public void Update()
    {
        // Random acceleration but it's not normalized!
        acceleration = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        // Normilize the acceletation
        acceleration.Normalize();
        // Now we can scale the magnitude as we wish!
        acceleration *= Random.Range(5f, 10f);

        // Speeds up the mover
        velocity += acceleration * Time.deltaTime; // Time.deltaTime is the time passed since the last frame.

        // Limit Velocity to the top speed
        velocity = Vector3.ClampMagnitude(velocity, topSpeed);

        // Moves the mover
        location += velocity * Time.deltaTime;


        // Updates the GameObject of this movement
        parent.transform.position = new Vector3(location.x, location.y,location.z);

    }
    public void CheckEdges()
    {
        location = body.position;

        if (location.x >= maximumPos.x)
        {
            location.x = maximumPos.x;
        }
        else if (location.x <= minimumPos.x)
        {
            location.x = minimumPos.x;
        }
        if (location.y >= maximumPos.y)
        {
            location.y = maximumPos.y;
        }
        else if (location.y <= minimumPos.y)
        {
            location.y = minimumPos.y;
        }
        if (location.z >= maximumPos.z)
        {
            location.z = maximumPos.z;
        }
        else if (location.z <= minimumPos.z)
        {
            location.z = minimumPos.z;
        }
    }

    private void findWindowLimits()
    {
        minimumPos = new Vector3(0, 15, 0);
        maximumPos = new Vector3(100, 25, 100);
    }
    }

public class oscillator
{

    // The basic properties of an oscillator class
    public Vector3 velocity, angle, amplitude;
    public float x, y, z;
    // The window limits
    private Vector3 maximumPos;

    // Gives the class a GameObject to draw on the screen
    public GameObject oGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    //Create variables for rendering the line between two vectors
    public LineRenderer lineRender;
    GameObject lineDrawing;
    public oscillator(Vector3 squidParent)
    {
        angle = Vector3.zero;
        velocity = new Vector3(Random.Range(-.008f, .008f), Random.Range(-.008f, .008f), Random.Range(-.008f, .008f));
        maximumPos = new Vector3(1, 1, 1);
        amplitude = new Vector3(Random.Range(-squidParent.x / 2, squidParent.x / 2), Random.Range(-squidParent.y / 2, squidParent.y / 2), Random.Range(-squidParent.z / 2, squidParent.z / 2));
        Renderer r = oGameObject.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
        GameObject lineDrawing = new GameObject();
        lineRender = lineDrawing.AddComponent<LineRenderer>();
        lineRender.material = new Material(Shader.Find("Diffuse"));
        r.material.color = Color.blue;

    }

   
}



