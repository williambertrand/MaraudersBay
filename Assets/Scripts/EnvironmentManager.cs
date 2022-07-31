using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{

    public GameObject waterPrefab;
    public Transform waterHolder;

    public int mapWidth;
    public int mapHeight;
    public float waterHeight;
    public float blockWidth;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWorldWater();

        // TODO: SPawn some initial gold or treasure to pick up
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnWorldWater()
    {
        for (int i = -mapWidth / 2; i < mapWidth / 2; i++)
        {
            for (int j = -mapHeight / 2; j < mapHeight / 2; j++)
            {
                SpawnWaterBlock(i, j);
            }
        }
    }

    void SpawnWaterBlock(int x, int y)
    {
        Vector3 pos = new Vector3(x * blockWidth, waterHeight, y * blockWidth);
        GameObject water = Instantiate(waterPrefab, pos, Quaternion.identity);
        water.transform.parent = waterHolder;
    }
}
