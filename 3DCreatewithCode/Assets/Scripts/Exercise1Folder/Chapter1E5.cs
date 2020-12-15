using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1E5 : MonoBehaviour
{
    // Declare a mover object
    private Mover1_8 mover;
    
    // Start is called before the first frame update
    void Start()
    {
        // Create a Mover object
        mover = new Mover1_8();
    }

    // Update is called once per frame forever and ever (until you quit).
    void Update()
    {
        mover.Update();
        mover.CheckEdges();
    }
}

public class Mover1_8
{
    // The basic properties of a mover class
    private Vector2 location, velocity, acceleration;
    private float topSpeed; 
    public float horizontalInput;
    public float verticalInput;
    // The window limits
    private Vector2 minimumPos, maximumPos;


    // Gives the class a GameObject to draw on the screen
    private GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Cube);

    public Mover1_8()
    {
        findWindowLimits();
        location = Vector2.zero; // Vector2.zero is a (0, 0) vector
        velocity = Vector2.zero;
        acceleration = new Vector2(5F, 0F);
        topSpeed = 2F;

        //We need to create a new material for WebGL
        Renderer r = mover.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
    }

    public void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //if (Input.GetKey("a"))
        //{
        //    velocity += acceleration * Time.deltaTime;
        //    velocity = Vector2.ClampMagnitude(velocity, topSpeed);
        //    location += velocity * Time.deltaTime;

        //}

        //if (Input.GetKey("d"))
        //{
        //    velocity -= acceleration * Time.deltaTime;
        //    velocity = Vector2.ClampMagnitude(velocity, topSpeed);
        //    location += velocity * Time.deltaTime;

        //}

        //mover.transform.position = new Vector2(location.x, location.y);
        mover.transform.Translate(Vector3.right * Time.deltaTime * acceleration * horizontalInput);
        mover.transform.Translate(Vector3.forward * Time.deltaTime * acceleration * verticalInput);
        mover.transform.Rotate(Vector3.up, Time.deltaTime * verticalInput);
    }

    public void CheckEdges()
    {
        if (location.x > maximumPos.x)
        {
            location.x -= maximumPos.x - minimumPos.x;
        }
        else if (location.x < minimumPos.x)
        {
            location.x += maximumPos.x - minimumPos.x;
        }
        if (location.y > maximumPos.y)
        {
            location.y -= maximumPos.y - minimumPos.y;
        }
        else if (location.y < minimumPos.y)
        {
            location.y += maximumPos.y - minimumPos.y;
        }
    }

    private void findWindowLimits()
    {
        // The code to find the information on the camera as seen in Figure 1.2

        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;
        // Next we grab the minimum and maximum position for the screen
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}

