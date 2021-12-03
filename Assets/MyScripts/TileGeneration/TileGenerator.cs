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
    private GameObject obstacleHolderInstance;
    private GameObject obstacleInstance;
    private int obstacleAmount;
    private int obstacleType;
    private Vector3 genPos;

    [Header("Coin")]
    [SerializeField] private GameObject coinHolder;
    [SerializeField] public GameObject coin;
    [SerializeField] private float distance = 8;
    private GameObject coinHolderInstance;
    private GameObject coinInstance;
    private int coinAmount;
    private Vector3 coinPos;

    [Header("Skybox")] 
    [SerializeField] private Material skyboxDay;
    [SerializeField] private Material skyboxNight;

    [Header("Other Stuff")]
    [SerializeField] private GameObject character;
    private int generationCount;
    private int generationAmount;

    void Start()
    {
        if (Random.value > 0.5f)
            RenderSettings.skybox = skyboxDay;
        else
            RenderSettings.skybox = skyboxNight;
        generationCount = 0;
        tileAmount = tiles.Count;
        obstacleAmount = obstacles.Count;
        generationAmount = 4;
        initialTile.name = $"#{generationCount} Tile Generated";
        obstacleHolder.name = $"#{generationCount} Obstacle Generated";
        coinHolder.name = $"#{generationCount} Coin Generated";
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

            obstacleHolderInstance = Instantiate(obstacleHolder, new Vector3(obstacleHolder.transform.position.x, 0, generationCount * 110),
                obstacleHolder.transform.rotation);

            coinHolderInstance = Instantiate(coinHolder, new Vector3(coinHolder.transform.position.x, 0, generationCount * 110),
                coinHolder.transform.rotation);

            tileInstance.name = $"#{generationCount} Tile Generated";
            obstacleHolderInstance.name = $"#{generationCount} Obstacle Generated";
            coinHolderInstance.name = $"#{generationCount} Coin Generated";

            Destroy(GameObject.Find($"#{generationCount - 2} Tile Generated"));
            if (generationCount > 2)
            {
                Destroy(GameObject.Find($"#{generationCount - 2} Obstacle Generated"));
                Destroy(GameObject.Find($"#{generationCount - 2} Coin Generated"));
            }
            if (generationCount >= 1)
            {
                GenerateObstacle();
                GenerateCoin();
            }
        }
    }

    private void GenerateCoin()
    {
        // Generate on z = 145, 172.5, 200, 227.5 (#tileG * 110 + 10 + 27.5 *i)
        for (int i = 0; i < generationAmount; i++)
        {
            // number of objects to generate on that line, min 1 max 2
            //int coinGenerateAmount = Random.Range(1, 3);
            //if (coinGenerateAmount == 1)
            //{
            //    GenerateOneCoinLine(i);
            //}
            //else if (coinGenerateAmount == 2)
            //{
            //    GenerateTwoCoinLine(i);
            //}
            GenerateOneCoinLine(i);
        }
    }

    private void GenerateOneCoinLine(int i)
    {
        // random position between three lanes, 0 left 1 middile 2 right
        int ranLane = Random.Range(0, 3);
        coinPos = new Vector3(-2.5f + ranLane * 2.5f, 0.35f, generationCount * 110 + distance + i * 27.5f);
        coinInstance = Instantiate(coin, coinPos, Quaternion.identity * Quaternion.Euler(0f, 0f, 0f));
        coinInstance.transform.parent = GameObject.Find($"#{generationCount} Coin Generated").transform;
    }

    //private void GenerateTwoCoinLine(int i)
    //{
    //    // random position between three lanes, 0 left 1 middile 2 right
    //    int ranLane = Random.Range(0, 3);
    //    for (int j = 0; j < 3; j++)
    //    {
    //        coinPos = new Vector3(-2.5f + ranLane * 2.5f, 0.5f, generationCount * 110 + 10 + i * 27.5f);
    //        obstacleInstance.transform.parent = GameObject.Find($"#{generationCount} Coin Generated").transform;
    //    }
    //}

    private void GenerateObstacle()
    {
        // Generate on z = 137.5, 165, 192.5, 220 (#tileG * 110 + 27.5 *i)
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
        if (obstacle.gameObject.name.Contains("Truck") || obstacle.gameObject.name.Contains("Bus"))
        {
            genPos = new Vector3(-2.5f + ranLane * 2.5f, 1.6f, generationCount * 110 + i * 27.5f);
            // clone object from prefab file and assign it to newObject
            obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
        }
        else if(obstacle.gameObject.name.Contains("Car"))
        {
            genPos = new Vector3(-2.5f + ranLane * 2.5f, 0.8f, generationCount * 110 + i * 27.5f);
            // clone object from prefab file and assign it to newObject
            obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
        }
        else if(obstacle.gameObject.name.Contains("Obstacle"))
        {
            genPos = new Vector3(-2.5f + ranLane * 2.5f, 0, generationCount * 110 + i * 27.5f);
            // clone object from prefab file and assign it to newObject
            obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 0f, 0f));
        }
      
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
                if (obstacle.gameObject.name.Contains("Truck") || obstacle.gameObject.name.Contains("Bus"))
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 1.6f, generationCount * 110 + i * 27.5f);
                    // clone object from prefab file and assign it to newObject
                    obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
                }
                else if (obstacle.gameObject.name.Contains("Car"))
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 0.8f, generationCount * 110 + i * 27.5f);
                    // clone object from prefab file and assign it to newObject
                    obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
                }
                else if(obstacle.gameObject.name.Contains("Obstacle"))
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 0, generationCount * 110 + i * 27.5f);
                    // clone object from prefab file and assign it to newObject
                    obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 0f, 0f));
                }
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
                if (obstacle.gameObject.name.Contains("Truck") || obstacle.gameObject.name.Contains("Bus"))
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 1.6f, generationCount * 110 + i * 27.5f);
                    // clone object from prefab file and assign it to newObject
                    obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
                }
                else if (obstacle.gameObject.name.Contains("Car"))
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 0.8f, generationCount * 110 + i * 27.5f);
                    // clone object from prefab file and assign it to newObject
                    obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
                }
                else if (obstacle.gameObject.name.Contains("Obstacle"))
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 0, generationCount * 110 + i * 27.5f);
                    // clone object from prefab file and assign it to newObject
                    obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 0f, 0f));
                }
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
                if (obstacle.gameObject.name.Contains("Truck") || obstacle.gameObject.name.Contains("Bus"))
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 1.6f, generationCount * 110 + i * 27.5f);
                    // clone object from prefab file and assign it to newObject
                    obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
                }
                else if (obstacle.gameObject.name.Contains("Car"))
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 0.8f, generationCount * 110 + i * 27.5f);
                    // clone object from prefab file and assign it to newObject
                    obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
                }
                else if (obstacle.gameObject.name.Contains("Obstacle"))
                {
                    genPos = new Vector3(-2.5f + j * 2.5f, 0, generationCount * 110 + i * 27.5f);
                    // clone object from prefab file and assign it to newObject
                    obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 0f, 0f));
                }
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
            if (obstacle.gameObject.name.Contains("Truck") || obstacle.gameObject.name.Contains("Bus"))
            {
                genPos = new Vector3(-2.5f + j * 2.5f, 1.6f, generationCount * 110 + i * 27.5f);
                // clone object from prefab file and assign it to newObject
                obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
            }
            else if (obstacle.gameObject.name.Contains("Car"))
            {
                genPos = new Vector3(-2.5f + j * 2.5f, 0.8f, generationCount * 110 + i * 27.5f);
                // clone object from prefab file and assign it to newObject
                obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
            }
            else if (obstacle.gameObject.name.Contains("Obstacle"))
            {
                genPos = new Vector3(-2.5f + j * 2.5f, 0, generationCount * 110 + i * 27.5f);
                // clone object from prefab file and assign it to newObject
                obstacleInstance = Instantiate(obstacle, genPos, Quaternion.identity * Quaternion.Euler(0f, 0f, 0f));
            }
            obstacleInstance.transform.parent = GameObject.Find($"#{generationCount} Obstacle Generated").transform;
        }
    }
}

