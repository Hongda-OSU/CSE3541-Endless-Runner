using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public BuildingGenerator buildingGenerator;

    // count of checkpoints the character has reached
    public int count;

    // List of cars to read from
    private List<string> carList;
    private List<string> busList;

    // Queue of car positions in the scene
    public Queue<Vector3> carPos;

    public void Initialize(List<string> carList,List<string> busList,Queue<Vector3> carPos){
        this.carList=carList;
        this.busList=busList;
        this.carPos=carPos;
    }

    // Start is called before the first frame update
    void Start()
    {
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
        int generateAmount = Random.Range(5, 7);

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

                    // position to be generated
                    Vector3 genPos= new Vector3(-2.5f + ranLane * 2.5f, 0.8f, count * 120 + i * 20);

                    // adjust height of different prefab
                    if (carList[ran].StartsWith("Truck"))
                    {
                        genPos.y=1.8f;
                    }
                    else if (carList[ran].StartsWith("Car_4"))
                    {
                        genPos.y=1.3f;
                    }
                    else if (carList[ran].StartsWith("Car_5"))
                    {
                        genPos.y=1f;
                    }

                    // clone object from prefab file and assign it to newObject
                    newObject = Instantiate((GameObject)LoadPrefabFromFile(carList[ran]), genPos , Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
   
                    // check if queue has this pos, if yes then regenerate
                    if (!carPos.Contains(new Vector3(newObject.transform.position.x, 2, newObject.transform.position.z)))
                    {
                        hasCar = false;
                    }
                    else
                    {
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
