using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2Fig7 : MonoBehaviour
{
    List<Mover2_7> movers = new List<Mover2_7>(); // Now we have multiple Movers!
    Attractor2_7 a;

    // Start is called before the first frame update
    void Start()
    {
        int numberOfMovers = 10;
        for (int i = 0; i < numberOfMovers; i++)
        {
            Vector2 randomLocation = new Vector2(Random.Range(-7f, 7f), Random.Range(-7f, 7f));
            Vector2 randomVelocity = new Vector2(Random.Range(0f, 5f), Random.Range(0f, 5f));
            Mover2_7 m = new Mover2_7(Random.Range(0.2f, 1f), randomVelocity, randomLocation); //Each Mover is initialized randomly.
            movers.Add(m);
        }
        a = new Attractor2_7();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Mover2_7 m in movers)
        {
            Rigidbody body = m.body;
            Vector2 force = a.Attract(body); // Apply the attraction from the Attractor on each Mover object

            m.ApplyForce(force);
            m.Update();
        }
    }
}


public class Attractor2_7
{
    public float mass;
    private Vector3 location;
    private float G;
    public Rigidbody body;
    private GameObject attractor;
    private float radius;

    public Attractor2_7()
    {
        attractor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject.Destroy(attractor.GetComponent<SphereCollider>());
        Renderer renderer = attractor.GetComponent<Renderer>();
        body = attractor.AddComponent<Rigidbody>();
        body.position = Vector3.zero;
        attractor.GetComponent<MeshRenderer>().enabled = false;
        // Generate a radius
        radius = 2;

        // Place our mover at the specified spawn position relative
        // to the bottom of the sphere
        attractor.transform.position = body.position;

        // The default diameter of the sphere is one unit
        // This means we have to multiple the radius by two when scaling it up
        attractor.transform.localScale = 2 * radius * Vector3.one;

        // We need to calculate the mass of the sphere.
        // Assuming the sphere is of even density throughout,
        // the mass will be proportional to the volume.
        body.mass = (4f / 3f) * Mathf.PI * radius * radius * radius;
        body.useGravity = false;
        body.isKinematic = true;

        renderer.material = new Material(Shader.Find("Diffuse"));
        renderer.material.color = Color.red;

        G = 19.8f;


    }

    public void update() {
        attractor.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    public Vector3 Attract(Rigidbody m)
    {
        Vector3 force = body.position - m.position;
        float distance = force.magnitude;

        // Remember we need to constrain the distance so that our circle doesn't spin out of control
        distance = Mathf.Clamp(distance, 5f, 20f);

        force.Normalize();
        float strength = (G * body.mass * m.mass) / (distance * distance);
        force *= strength;
        return force;
    }
}

public class Mover2_7
{
    // The basic properties of a mover class
    public Transform transform;
    public Rigidbody body;

    Vector3 minPosition, maxPosition, initPos;
    public Vector3 location;
    private GameObject mover;

    public Mover2_7(float randomMass, Vector3 initialVelocity, Vector3 initialPosition)
    {
        mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject.Destroy(mover.GetComponent<SphereCollider>());
        transform = mover.transform;
        mover.AddComponent<Rigidbody>();
        body = mover.GetComponent<Rigidbody>();
        body.useGravity = false;
        Renderer renderer = mover.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.SetColor("_Color", new Vector4(.4f, .6f, 1f));
        renderer.material.EnableKeyword("_EMISSION");
        renderer.material.SetColor("_EmissionColor", new Vector4(.8f, .6f, 1f));
        mover.transform.localScale = new Vector3(randomMass, randomMass, randomMass);
        initPos = initialPosition;
        body.mass = 1;
        body.position = initialPosition; // Default location
        body.velocity = initialVelocity; // The extra velocity makes the mover orbit
        minPosition = new Vector3(0, 12, 0);
        maxPosition = new Vector3(80, 20, 80);
        location = initPos;




    }

    public void ApplyForce(Vector3 force)
    {
        body.AddForce(force, ForceMode.Force);
    }

    public void Update()
    {
        //body.AddForce(Vector3.MoveTowards(location, initPos, 10f));
        location = body.transform.position;
        //body.transform.position = Vector3.MoveTowards(location, initPos, 100000f * Time.deltaTime);
        body.AddForce((initPos- location) * Vector3.Distance(initPos,location), ForceMode.Force);
       // checkEdges();
        
    }

    public void checkEdges()
    {
        if (location.x > maxPosition.x)
        {
            location.x -= maxPosition.x - minPosition.x;
        }
        else if (location.x < minPosition.x)
        {
            location.x += maxPosition.x - minPosition.x;
        }
        if (location.y > maxPosition.y)
        {
            location.y -= maxPosition.y - minPosition.y;
        }
        else if (location.y < minPosition.y)
        {
            location.y += maxPosition.y - minPosition.y;
        }
        if (location.z > maxPosition.z)
        {
            location.z -= maxPosition.z - minPosition.z;
        }
        else if (location.z < minPosition.z)
        {
            location.z += maxPosition.z - minPosition.z;
        }
        body.transform.position = location;
    }

    
}