using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baton
{

    public GameObject Baton;

    // We are going to rotate on the z-axis. We'll rename this as a velocity;
    public Vector3 aVelocity;
    float speed, offset;
    Vector3 pos, init;
    // Start is called before the first frame update
    public baton(Vector3 location)
    {
        Baton = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Baton.transform.position = location;
        Baton.transform.localScale = new Vector3(.2f,1f,.2f);
        Renderer renderer = Baton.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Diffuse"));
        speed = Random.Range(.5f,2f);
        offset = Random.Range(0,Mathf.PI*2);
        renderer.material.color = new Vector4(1f,5f,2f);
        aVelocity = new Vector3(0f, 0f, 6f);
        init = location;
    }

    // Update is called once per frame
    public void update()
    {
        //Baton.transform.Rotate(aVelocity*Time.deltaTime, Space.World);
        //Baton.transform.rotation += aVelocity;
        pos = init;
        pos.y+= Mathf.Sin(Time.time*speed+offset)*2f;
        pos.y=Mathf.Min(pos.y, (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, init) - 2.53f)/2);
        //Debug.Log(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, init));

        Baton.transform.position = pos;
    }
}