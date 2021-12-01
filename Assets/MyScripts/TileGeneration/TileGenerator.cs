using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField] public List<GameObject> tiles;
    [SerializeField] private GameObject initialTile;
    private GameObject tileInstance;
    private int tileAmount;
    private int tileType;

    [Header("Obstacle")]
    [SerializeField] private GameObject obstacleHolder;
    [SerializeField] public List<GameObject> obstacles;
    private GameObject obstacleGeneratorInstance;
    private GameObject obstacleInstance;
    private int obstacleAmount;
    private int obstacleType;
    private Vector3 genPos;
    private int generationAmount;

   [Header("Other Stuff")]
    [SerializeField] private GameObject character;
    private int generationCount;

    void Start()
    {
        generationCount = 0;
        tileAmount = tiles.Count;
        obstacleAmount = obstacles.Count;
        generationAmount = 4;
        initialTile.name = $"#{generationCount} Tile Generated";
        obstacleHolder.name = $"#{generationCount} Obstacle Generated";
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

            obstacleGeneratorInstance = Instantiate(obstacleHolder, new Vector3(obstacleHolder.transform.position.x, 0, generationCount * 110),
                obstacleHolder.transform.rotation);

            tileInstance.name = $"#{generationCount} Tile Generated";
            obstacleGeneratorInstance.name = $"#{generationCount} Obstacle Generated";

            Destroy(GameObject.Find($"#{generationCount - 2} Tile Generated"));
            if (generationCount > 2)
            {
                Destroy(GameObject.Find($"#{generationCount - 2} Obstacle Generated"));
            }
            if (generationCount >= 1)
            {
                GenerateObstacle();
            }
        }
    }

    private void GenerateObstacle()
    {
        // Generate on z = 137.5, 165, 192.5, 220
        for (int i = 0; i < generationAmount; i++)
        {
            // number of objects to generate on that line, min 1 max 3
            int obGenerateAmount = Random.Range(1, 4);
            if (obGenerateAmount == 1)
            {
                GenerateOneObstacle(i);
            }
            else if (obGenerateAmount == 2)
            {
                GenerateTwoObstacles(i);
            }
            else if (obGenerateAmount == 3)
            {
                GenerateThreeObstacle(i);
            }
        }
      
    }

    private void GenerateOneObstacle(int i)
    {
        obstacleType = Random.Range(0, obstacleAmount);
        GameObject obstacle = obstacles[obstacleType];
        // random position between three lanes, 0 left 1 middile 2 right
        int ranLane = Random.Range(0, 3);
        // position to be generated
        if (obstacle.GetComponent<BoxCollider>().tag == "Truck" || obstacle.GetComponent<BoxCollider>().tag == "Bus")
        {
            genPos = new Vector3(-2.5f + ranLane * 2.5f, 1.6f, generationCount * 110 + i * 27.5f);
        }
        else
        {
            genPos = new Vector3(-2.5f + ranLane * 2.5f, 0.8f, generationCount * 110 + i * 27.5f);
        }

        // clone object from prefab file and assign it to newObject
        obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
        obstacleInstance.transform.parent = GameObject.Find($"#{generationCount} Obstacle Generated").transform;
    }

    private void GenerateTwoObstacles(int i)
    {
        int rndForm = Random.Range(1, 4);
        if (rndForm == 1)
        {
            // left, center
            for (int j = 0; j < 2; j++)
            {
                obstacleType = Random.Range(0, obstacleAmount);
                GameObject obstacle = obstacles[obstacleType];
                // position to be generated
                if (obstacle.GetComponent<BoxCollider>().tag == "Truck" || obstacle.GetComponent<BoxCollider>().tag == "Bus")
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 1.6f, generationCount * 110 + i * 27.5f);
                }
                else
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 0.8f, generationCount * 110 + i * 27.5f);
                }

                // clone object from prefab file and assign it to newObject
                obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
                obstacleInstance.transform.parent = GameObject.Find($"#{generationCount} Obstacle Generated").transform;
            }
        }
        if (rndForm == 2)
        {
            // center, right
            for (int j = 1; j < 3; j++)
            {
                obstacleType = Random.Range(0, obstacleAmount);
                GameObject obstacle = obstacles[obstacleType];
                // position to be generated
                if (obstacle.GetComponent<BoxCollider>().tag == "Truck" || obstacle.GetComponent<BoxCollider>().tag == "Bus")
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 1.6f, generationCount * 110 + i * 27.5f);
                }
                else
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 0.8f, generationCount * 110 + i * 27.5f);
                }

                // clone object from prefab file and assign it to newObject
                obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
                obstacleInstance.transform.parent = GameObject.Find($"#{generationCount} Obstacle Generated").transform;
            }
        }
        if (rndForm == 3)
        {
            // left, right
            for (int j = 0; j < 3; j+=2)
            {
                obstacleType = Random.Range(0, obstacleAmount);
                GameObject obstacle = obstacles[obstacleType];
                // position to be generated
                if (obstacle.GetComponent<BoxCollider>().tag == "Truck" || obstacle.GetComponent<BoxCollider>().tag == "Bus")
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 1.6f, generationCount * 110 + i * 27.5f);
                }
                else
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 0.8f, generationCount * 110 + i * 27.5f);
                }

                // clone object from prefab file and assign it to newObject
                obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
                obstacleInstance.transform.parent = GameObject.Find($"#{generationCount} Obstacle Generated").transform;
            }
        }
    }

    private void GenerateThreeObstacle(int i)
    {
        for(int j = 0; j < 3; j++)
        {
            obstacleType = Random.Range(0, obstacleAmount);
            GameObject obstacle = obstacles[obstacleType];
            // position to be generated
            if (obstacle.GetComponent<BoxCollider>().tag == "Truck" || obstacle.GetComponent<BoxCollider>().tag == "Bus")
            {
                genPos = new Vector3(-2.5f + j * 2.5f, 1.6f, generationCount * 110 + i * 27.5f);
            }
            else
            {
                genPos = new Vector3(-2.5f + j * 2.5f, 0.8f, generationCount * 110 + i * 27.5f);
            }

            // clone object from prefab file and assign it to newObject
            obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
            obstacleInstance.transform.parent = GameObject.Find($"#{generationCount} Obstacle Generated").transform;
        }
    }
}

