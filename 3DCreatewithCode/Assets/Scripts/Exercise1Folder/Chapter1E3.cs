using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1E3 : MonoBehaviour
{
    // Variables for the location and speed of mover
    private Vector3 location, velocity, acceleration;

    // Variables to limit the mover within the screen space
    private Vector3 minimumPos, maximumPos;

    // A Variable to represent our mover in the scene
    private GameObject mover;

    float topSpeed = .1F;

    // Start is called before the first frame update
    void Start()
    {
        // We want to start by setting the camera's projection to Orthographic mode
        //Camera.main.orthographic = true;

        // Next we grab the minimum and maximum position for the screen
        minimumPos = new Vector3(0f, 0f, 0f);
        maximumPos = new Vector3(5f, 5f, 5f);


        // We now can set the mover as a primitive sphere in unity
        mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //We need to create a new material for WebGL
        Renderer r = mover.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
    }

    // Update is called once per frame forever and ever (until you quit).
    void Update()
    {
        // Each frame, we will check to see if the mover has touched a boarder
        // We check if the X/Y position is greater than the max position OR if it's less than the minimum position
        bool xHitBorder = location.x > maximumPos.x || location.x < minimumPos.x;
        bool yHitBorder = location.y > maximumPos.y || location.y < minimumPos.y;
        bool zHitBorder = location.z > maximumPos.z|| location.z < minimumPos.z;

        if (xHitBorder)
        {
            velocity.x = -velocity.x;
        }

        if (yHitBorder)
        {
            velocity.y = -velocity.y;
        }

        if(zHitBorder)
        {
            velocity.z = -velocity.z;
        }

        // Lets now update the location of the mover
        location += velocity;
        acceleration = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        velocity += acceleration * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, topSpeed);
        // Now we apply the positions to the mover to put it in it's place
        mover.transform.position = new Vector3(location.x, location.y, location.z);
    }
}