using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinTerain : MonoBehaviour
{
    public List<Vector3> terrainArray = new List<Vector3>();
    public GameObject terrainObject;
    public int cols, rows;
    public Color color1, color2, color3, color4, color5;

    
    //public int animal private void OnEnable()
    //{

    //}
    // Start is called before the first frame update


    void Start()
    {
        GameObject terrain = new GameObject();
        terrain.name = "Terrain";
        float xOff = 0;
        for (int i = 0; i < cols; i++)
        {
            float yOff = 0;
            for (int j = 0; j < rows; j++)
            {
                float theta = ExtensionMethods.Remap(Mathf.PerlinNoise(xOff, yOff), 0, 1, 0, 7);
                float theta2 = ExtensionMethods.Remap(Mathf.PerlinNoise(xOff, yOff), 0, 1, 0, 6);
                Quaternion perlinRotation = new Quaternion();
                Vector3 perlinVectors = new Vector3(Mathf.Cos(theta2), Mathf.Sin(theta2), 0);
                perlinRotation.eulerAngles = perlinVectors * 100;

                terrainObject = Instantiate(terrainObject, new Vector3(i, theta, j), perlinRotation);
                terrainObject.transform.SetParent(terrain.transform);
                Renderer terrainRenderer = terrainObject.GetComponent<Renderer>();
                terrainRenderer.material.SetColor("_Color", colorTerrain(terrainObject.transform.position));
                yOff += .06f;
            }
            xOff += 0.06f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color colorTerrain(Vector3 terrainCubePosition)
    {
        Color terrainColor = new Vector4(1, 1, 1);

        if (terrainCubePosition.y >0 && terrainCubePosition.y <=2)
        {
            terrainColor = color1;
        }
        else if (terrainCubePosition.y > 2 && terrainCubePosition.y <= 3.5)
        {
            terrainColor = color2;
        }
        else if (terrainCubePosition.y > 3.5 && terrainCubePosition.y <= 5)
        {
            terrainColor = color3;
        }
        else if (terrainCubePosition.y > 5 && terrainCubePosition.y <= 6.5)
        {
            terrainColor = color4;
        }
        else if (terrainCubePosition.y > 6.5 && terrainCubePosition.y <= 8)
        {
            terrainColor = color5;
        }
        return terrainColor;
    }



}
