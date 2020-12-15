using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addCreatures : MonoBehaviour
{
    // Start is called before the first frame update
    public int OctopusCount;
    public int WispCount;
    public int WormCount;
    public int FishCount;
    public int randomFishCount;
    public int SquidCount = 10;
    List<chapter1Creature> octopi = new List<chapter1Creature>();
    GameObject creature;
    chapter1Creature octopus;

    List<Mover2_7> movers = new List<Mover2_7>(); // Now we have multiple Movers!
    Attractor2_7 a;

    List<baton> worms = new List<baton>();


    List<Movers> fish = new List<Movers>();
    fishLead fishLead;
    
    List<randomFish> fishR = new List<randomFish>();

    randomFish2 randomFish2;
    
    List<oscillator> oscillate = new List<oscillator>();
    List<randomFish2> squid = new List<randomFish2>();
    void Start()
    {
       //octopi
        for (int i = 0; i < OctopusCount; i++)
        {
            octopus = new chapter1Creature();
            octopi.Add(octopus);
        }
        //wisps
        for (int i = 0; i < WispCount; i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(0, 100), Random.Range(3, 100), Random.Range(0, 100));
            Vector3 randomVelocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            Mover2_7 m = new Mover2_7(Random.Range(0.2f, .5f), randomVelocity, randomLocation); //Each Mover is initialized randomly.
            movers.Add(m);
        }
        a = new Attractor2_7();
        //worms
        for (int i = 0; i< WormCount; i++) {
            float x = Random.Range(0, 100);
            float z = Random.Range(0, 100);
            float y = ExtensionMethods.Remap(Mathf.PerlinNoise(x*.06f, z * .06f), 0f, 1f, 0f, 7f);
            worms.Add(new baton(new Vector3(x,y-1f,z)));
        }
        //fish
        for (int i = 0; i < FishCount; i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(50f, 100f), Random.Range(50f, 100f), Random.Range(50f, 20f));
            Vector3 randomVelocity = new Vector3(Random.Range(0f, 3f), Random.Range(0f, 3f), Random.Range(0f, 3f));
            Movers m = new Movers(Random.Range(0.2f, 1f), randomVelocity, randomLocation); //Each Mover is initialized randomly.
            fish.Add(m);
        }
        fishLead = new fishLead();


        //randomFish
        for (int i = 0; i < randomFishCount; i++)
        {
            randomFish a =new randomFish();
            fishR.Add(a);
        }


        //squid
        randomFish2 = new randomFish2();
        while (oscillate.Count< SquidCount)
        {
            oscillator o = new oscillator(new Vector3(2f, 2f, 2f));
            o.oGameObject.transform.SetParent(randomFish2.body.transform);
            oscillate.Add(o);
            Debug.Log("Squid instantiated");
            squid.Add(randomFish2);
        }
       
        
    }

    // Update is called once per frame
    void Update()
    {
        // octopi
        foreach (chapter1Creature o in octopi)
        {
            o.move();
            o.checkEdges();
        }
        //wisps
        foreach (Mover2_7 m in movers)
        {
            Rigidbody body = m.body;
            Vector3 force = a.Attract(body); // Apply the attraction from the Attractor on each Mover object

            m.ApplyForce(force);
            m.Update();
        }
        a.update();
        //worms
        foreach (baton w in worms)
        {
            w.update();
        }

        //fish
        foreach (Movers m in fish)
        {
            Rigidbody body = m.body;
            Vector3 force = fishLead.Attract(body); // Apply the attraction from the Attractor on each Mover object

            m.ApplyForce(force);
            m.Update();
        }

        fishLead.Update();
        fishLead.CheckEdges();

        //randomFish
        foreach (randomFish fish in fishR)
        {
            fish.Update();
            fish.CheckEdges();
        }
        //squid
        foreach (oscillator o in oscillate)
        {
            o.x = Mathf.Sin(o.angle.x) * o.amplitude.x;
            o.y = Mathf.Sin(o.angle.y) * o.amplitude.y;
            o.z = Mathf.Sin(o.angle.z) * o.amplitude.z;
            o.angle += o.velocity;
            o.lineRender.SetPosition(1, o.oGameObject.transform.position);
            o.lineRender.SetPosition(0, randomFish2.body.transform.position);
            o.oGameObject.transform.transform.Translate(new Vector3(o.x, o.y, o.z) * Time.deltaTime);

            Vector3 difference = randomFish2.body.transform.position - o.oGameObject.transform.position;
            float m = difference.magnitude;
            if (m <= 1f)
            {
                o.oGameObject.transform.transform.Translate(new Vector3(o.x, o.y, o.z) * Time.deltaTime);
            }
            else
            {
                //o.angle -= o.velocity;
                o.oGameObject.transform.transform.Translate(new Vector3(o.x, o.y, o.z) * Time.deltaTime);
            }
            randomFish2.Update();
            randomFish2.CheckEdges();
        }
        
    }
}
