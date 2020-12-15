using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chapter1Creature
{
    // Start is called before the first frame update
    Vector3 location, acceleration, velocity;
    float topSpeed, baseSpeed, burstSpeed;
    
    Vector3 minPosition, maxPosition;
    List<chapter1Creature> octopi = new List<chapter1Creature>();
    GameObject octopus = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    bool burst;
    Vector3 burstAcceleration;
    float burstCount, burstCutoff;
    public chapter1Creature()
    {

        minPosition = new Vector3(0, 10, 0);
        maxPosition = new Vector3(80,20,80);
        location = new Vector3(Random.Range(0, 80), Random.Range(4, 20), Random.Range(0, 80)); // Vector2.zero is a (0, 0) vector
        velocity = Vector3.zero;
        acceleration = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        topSpeed = 3F;
        baseSpeed = 5f;
        burstSpeed = 80f;
        burst = false;
        burstCount = 0;
        burstCutoff = Random.Range(10,20);
        //burstCutoff = 1;
        // this.ExecRoutine(accelerationState());
    }


    public void move() {


        //acceleration+=new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));

        burstCount+=Time.deltaTime;

        if (!burst)
        {
            acceleration = new Vector3(Mathf.PerlinNoise(location.x, location.y) * 2 - 1, Mathf.PerlinNoise(location.y, location.z) * 2 - 1, Mathf.PerlinNoise(location.z, location.x) * 2 - 1);
            velocity += acceleration * Time.deltaTime; // Time.deltaTime is the time passed since the last frame.

            // Limit Velocity to the top speed

            velocity = Vector3.ClampMagnitude(velocity, topSpeed);
            if (burstCount >= burstCutoff)
            {
                burst = true;
                acceleration = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f));
                burstCount = 0;
                burstCutoff /= 15;
                //Debug.Log("burst!");
            }
        }
        else {
            velocity += acceleration * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, burstSpeed);
            if (burstCount >= burstCutoff)
            {
                burst = false;
                acceleration = Vector3.zero;
                burstCount = 0;
                burstCutoff = Random.Range(10, 20);
                //Debug.Log("stop");
            }
        }
        // Moves the mover
        location += velocity * Time.deltaTime;
        octopus.transform.position = new Vector3(location.x, location.y, location.z);
        //Debug.Log("out of bounds: "+ location + " ," + velocity + " ," + acceleration);
        //Debug.Log("acceleration: "+acceleration);
        //if (velocity.y > 0) Debug.Log("up");
        //else Debug.Log("down");
    }

   /* public IEnumerator accelerationState() {
        
        Debug.Log("Top of the state");
        yield return new WaitForSeconds(2);
       this.ExecRoutine(accelerationAction());
    }

    public IEnumerator accelerationAction() {

        Vector2 slowAcceleration = new Vector2(.1f, 0f);
        Vector2 fastAcceleration = new Vector2(1f, 0f);
        Debug.Log("Top of action");
        acceleration = slowAcceleration;
        yield return new WaitForSeconds(3);
        acceleration = fastAcceleration;
        //MonoBehaviour.StartCoroutine(accelerationState());
        this.ExecRoutine(accelerationState());
    }
    public IEnumerator ExecRoutine(IEnumerator cor) { 
        Debug.Log("exec");
        while (cor.MoveNext())

            yield return cor.Current;

    }*/
   public void checkEdges() {
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
    }
    
    // Update is called once per frame
    
}
