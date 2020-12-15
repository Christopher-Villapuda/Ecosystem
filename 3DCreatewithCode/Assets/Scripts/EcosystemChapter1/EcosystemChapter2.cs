using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcosystemChapter2 : MonoBehaviour
{

    List<Movers> movers = new List<Movers>(); // Now we have multiple Movers!
    fishLead a;

    // Start is called before the first frame update
    void Start()
    {
        int numberOfMovers = 10;
        for (int i = 0; i < numberOfMovers; i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            Vector3 randomVelocity = new Vector3(Random.Range(0f, 3f), Random.Range(0f, 3f), Random.Range(0f, 3f));
            Movers m = new Movers(Random.Range(0.2f, 1f), randomVelocity, randomLocation); //Each Mover is initialized randomly.
            movers.Add(m);
        }
        a = new fishLead();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Movers m in movers)
        {
            Rigidbody body = m.body;
            Vector3 force = a.Attract(body); // Apply the attraction from the Attractor on each Mover object

            m.ApplyForce(force);
            m.Update();
        }

        a.Update();
        a.CheckEdges();
        
    }
}




public class fishLead
{
    private Vector3 location, velocity, acceleration;
    private float topSpeed;

    private Vector3 minimumPos, maximumPos;

    public GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    public float mass;
    private float G;
    private float radius;
    public Rigidbody body;
    public fishLead()
    {
        findLimits();
        body = mover.AddComponent<Rigidbody>();
        body.position = new Vector3(Random.Range(0, 100), Random.Range(11, 25), Random.Range(0, 100));
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        topSpeed = 2F;
        radius = 1.2f;
        mover.transform.position = body.position;
        mover.transform.localScale = 2 * radius * Vector3.one;
        body.mass = (4f / 3f) * Mathf.PI * radius * radius * radius;
        body.useGravity = false;
        body.isKinematic = true;
        G = 7f;
        Renderer renderer = mover.GetComponent<Renderer>();
        renderer.material.color = Color.blue;
    }

    public Vector3 Attract(Rigidbody m)
    {
        Vector3 force = body.position - m.position;
        float distance = force.magnitude;

        // Remember we need to constrain the distance so that our circle doesn't spin out of control
        distance = Mathf.Clamp(distance, 5f, 25f);

        force.Normalize();
        float strength = (G * body.mass * m.mass) / (distance * distance);
        force *= strength;
        return force;
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
        mover.transform.position = new Vector3(location.x, location.y, location.z);

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

    private void findLimits()
    {
        minimumPos = new Vector3(0, 10, 0);
        maximumPos = new Vector3(100, 20, 100);
    }

}

public class Movers
{
    // The basic properties of a mover class
    public Transform transform;
    public Rigidbody body;
    public Vector3 location;
    private Vector3 minimumPos, maximumPos;
    private GameObject mover;

    public Movers(float randomMass, Vector3 initialVelocity, Vector3 initialPosition)
    {
        mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject.Destroy(mover.GetComponent<SphereCollider>());
        transform = mover.transform;
        mover.AddComponent<Rigidbody>();
        body = mover.GetComponent<Rigidbody>();
        body.useGravity = false;
        Renderer renderer = mover.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Diffuse"));
        renderer.material.color = Color.red;
        mover.transform.localScale = new Vector3(randomMass, randomMass, randomMass);
        body.mass = 1;
        initialPosition = new Vector3(Random.Range(0, 100), Random.Range(10, 12), Random.Range(0, 100));
        body.position = initialPosition;// Default location
        body.velocity = initialVelocity; // The extra velocity makes the mover orbit
        findLimits();

    }

    public void ApplyForce(Vector3 force)
    {
        body.AddForce(force, ForceMode.Force);
    }

    public void Update()
    {
        CheckEdges();
    }

    public void CheckEdges()
    {
        Vector3 velocity = body.velocity;
        if (transform.position.x >= maximumPos.x  )
        {
            velocity.x *= -1 * Time.deltaTime;
        }
        if (transform.position.x <= minimumPos.x)
        {
            velocity.x *= 1 * Time.deltaTime;
        }
        if (transform.position.y >= maximumPos.y )
        {
            velocity.y *= -1 * Time.deltaTime;
        }
        if (transform.position.y <= minimumPos.y)
        {
            velocity.y *= 1 * Time.deltaTime;
        }
        if (transform.position.z >= maximumPos.z)
        {
            velocity.z *= -1 * Time.deltaTime;
        }
        if (transform.position.z <= minimumPos.z)
        {
            velocity.z *= 1 * Time.deltaTime;
        }
        body.velocity = velocity;

    }

    private void findLimits()
    {
        minimumPos = new Vector3(0, 10, 0);
        maximumPos = new Vector3(100, 25, 100);
    }
}
