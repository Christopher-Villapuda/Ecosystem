using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcosystemEngine : MonoBehaviour
{
    // This will run the Ecosystem
    //Each chapter will be instantiated here
    private randomFish fish;
    public float floorY;
    public float leftWallX;
    public float rightWallX;
    public float ceilingY;
    public Transform moverSpawnTransform;
    void Start()
    {
    //    fish = new randomFish(moverSpawnTransform.position, leftWallX, rightWallX, floorY, ceilingY);
    //    //Vector2 randomLocation = new Vector2(Random.Range(-7f, 7f), Random.Range(-7f, 7f));

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    f.step();
    //    fish.CheckBoundaries();
    }

}