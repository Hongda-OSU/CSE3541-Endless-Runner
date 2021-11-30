using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{

    public GameObject path;
    public BuildingGenerator buildingGenerator;

    // count of checkpoints the character has reached
    private int count;

    // List of cars to read from
    private string[] cars;
    private List<string> carList;

    // Queue of car positions in the scene
    private Queue<Vector3> carPos;

    // Start is called before the first frame update
    void Start()
    {
        cars = new string[]{"Car_1_Blue","Car_1_Green","Car_1_Purple","Car_1_Red","Car_1_Yellow","Car_1_Silver",
                        "Car_2_Blue","Car_2_Green","Car_2_Purple","Car_2_Red","Car_2_Yellow","Car_2_Silver",
                        "Car_3_Blue","Car_3_Green","Car_3_Purple","Car_3_Red","Car_3_Yellow","Car_3_Silver",
                        "Car_4_Blue","Car_4_Green","Car_4_Purple","Car_4_Red","Car_4_Yellow","Car_4_Silver",
                        "Car_5_Blue","Car_5_Green","Car_5_Purple","Car_5_Red","Car_5_Yellow","Car_5_Silver",
                        "Car_6_Blue","Car_6_Green","Car_6_Purple","Car_6_Red","Car_6_Yellow","Car_6_Silver",
                        "Truck_2_Blue","Truck_2_Green","Truck_2_Purple","Truck_2_Red","Truck_2_Yellow","Truck_2_Silver",
                        "Policecar"
                        };

        string[] bus = { "Bus_Blue", "Bus_Green", "Bus_Purple", "Bus_Red", "Bus_Silver", "Bus_Yellow" };

        carList = new List<string>(cars);
        List<string> busList = new List<string>(bus);

        count = buildingGenerator.count;

        carPos = new Queue<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateObstacle()
    {
        // choose random car in list to clone
        int ran = Random.Range(0, carList.Count);
        // number of objects to generate, min 4 max 5
        int generateAmount = Random.Range(4, 6);

        // update count from BuildingGenerator
        count = buildingGenerator.count;

        // if count>6 then double car obstacles
        int double_ob = 1;
        if (count > 0) double_ob = 2;

        for (int i = 0; i < generateAmount; i++)
        {
            for (int k = 0; k < double_ob; k++)
            {
                // remove old deleted car from queue
                if (carPos.Count > 5) carPos.Dequeue();

                ran = Random.Range(0, carList.Count);

                // check if current pos has car already
                bool hasCar = true;
                // new car to be created
                GameObject newObject = new GameObject();
                Destroy(newObject);

                while (hasCar)
                {
                    // random position between three lanes, 0 left 1 middile 2 right
                    int ranLane = Random.Range(0, 3);

                    // clone object from prefab file and assign it to newObject
                    newObject = Instantiate((GameObject)LoadPrefabFromFile(carList[ran]), new Vector3(-2.5f + ranLane * 2.5f, 0.8f, count * 120 + i * 20 - 90), Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));

                    // adjust height of different prefab
                    if (carList[ran].StartsWith("Truck"))
                    {
                        newObject.transform.position = new Vector3(-2.5f + ranLane * 2.5f, 1.8f, count * 120 + i * 20 - 90);
                    }
                    else if (carList[ran].StartsWith("Car_4"))
                    {
                        newObject.transform.position = new Vector3(-2.5f + ranLane * 2.5f, 1.3f, count * 120 + i * 20 - 90);
                    }
                    else if (carList[ran].StartsWith("Car_5"))
                    {
                        newObject.transform.position = new Vector3(-2.5f + ranLane * 2.5f, 1f, count * 120 + i * 20 - 90);
                    }

                    // check if queue has this pos, if yes then regenerate
                    if (!carPos.Contains(new Vector3(newObject.transform.position.x, 2, newObject.transform.position.z)))
                    {
                        hasCar = false;                       
                    }else{
                        // destroy old car
                        Destroy(newObject);
                    }
                }

                // add position of new generated car to queue
                carPos.Enqueue(new Vector3(newObject.transform.position.x, 2, newObject.transform.position.z));

                // assign object to path as child
                newObject.transform.parent = GameObject.Find("Path count:" + count).transform;
                newObject.AddComponent<BoxCollider>();
            }
        }


    }

    private UnityEngine.Object LoadPrefabFromFile(string filename)
    {
        // read from prefab file
        Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
        var loadedObject = Resources.Load("Cars/" + filename);

        return loadedObject;
    }
}
