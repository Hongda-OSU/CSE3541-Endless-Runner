using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] public List<GameObject> tiles;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject initialTile;
    //[SerializeField] public List<GameObject> obstacles;
    private GameObject tileInstance;
    private int tileAmount;
    private int generationCount;
    private int tileType;
    //private GameObject obstacleInstance;

    void Start()
    {
        generationCount = 0;
        tileAmount = tiles.Count;
    }

    void Update()
    {
        // detect if player has reached position for spawning next tiles
        if (character.transform.position.z > generationCount * 110 + 10)
        {
            // increment counter until player gets to next position to spawn
            generationCount++;
            tileType = Random.Range(0, tileAmount);
            // initialize the tile
            tileInstance = Instantiate(tiles[tileType], new Vector3(tiles[tileType].transform.position.x, 0, generationCount * 110),
                tiles[tileType].transform.rotation);
            tileInstance.name = $"#{generationCount} Tile Generated";
            if (generationCount == 2)
            {
                // destroy old path and building
                Destroy(initialTile);
            }
            else
            {
                Destroy(GameObject.Find($"#{generationCount - 2} Tile Generated"));
            }
            //GenerateObstacle();
        }
    }

    //private void GenerateObstacle()
    //{
    //    // number of objects to generate, min 4 max 5
    //    int generateAmount = Random.Range(5, 7);

    //    for (int i = 0; i < generateAmount; i++)
    //    {
    //        int ran = Random.Range(0, obstacles.Count);
    //        GameObject obstacle = obstacles[ran];
    //        // random position between three lanes, 0 left 1 middile 2 right
    //        int ranLane = Random.Range(0, 3);
    //        // position to be generated
    //        Vector3 genPos = new Vector3(-2.5f + ranLane * 2.5f, 0.8f, generationCount * 120 + i * 20);

    //        // clone object from prefab file and assign it to newObject
    //        obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
    //        obstacleInstance.transform.parent = obstacleToDestory.transform;
    //    }
    //}
}

