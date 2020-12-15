using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcosystemChapter1 : MonoBehaviour
{
    randomFish a;
    void Start()
    {
        a = new randomFish();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        a.Update();
        a.CheckEdges();

    }
}
public class randomFish
{
    private Vector3 location, velocity, acceleration;
    private float topSpeed;
    private Vector3 minimumPos, maximumPos;
    public GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    public float mass; 
    private float radius;
    public Rigidbody body;
    public Renderer myRenderer;
    public randomFish()
    {
        findLimits();
        body = mover.AddComponent<Rigidbody>();
        body.position = Vector3.zero;
        //location = new Vector3(50,10,50); 
        velocity = Vector3.zero;
        acceleration = Vector2.zero;
        topSpeed = 4F;
        radius = .5f;
        mover.transform.position = body.position;
        mover.transform.localScale = 2 * radius * Vector3.one;
        body.mass = (4f / 3f) * Mathf.PI * radius * radius * radius;
        body.useGravity = false;
        body.isKinematic = true;
        Renderer renderer = mover.GetComponent<Renderer>();
        renderer.material.color = Color.red;
    }

    public void Update()
    {
        acceleration = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        acceleration.Normalize();
        acceleration *= Random.Range(5f, 10f);
        velocity += acceleration * Time.deltaTime; 
        velocity = Vector3.ClampMagnitude(velocity, topSpeed);
        location += velocity * Time.deltaTime;
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
        maximumPos = new Vector3(100,20,100);
    }

}
