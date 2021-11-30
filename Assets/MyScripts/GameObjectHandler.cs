using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectHandler : MonoBehaviour
{
    [SerializeField] BuildingGenerator buildingGenerator;
    [SerializeField] ObstacleGenerator obstacleGenerator;

    private string[] cars;
    private List<string> carList;

    private string[] bus;
    private List<string> busList;
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

        bus = new string[]{ "Bus_Blue", "Bus_Green", "Bus_Purple", "Bus_Red", "Bus_Silver", "Bus_Yellow" };

        carList = new List<string>(cars);
        busList = new List<string>(bus);
        carPos = new Queue<Vector3>();

        obstacleGenerator.count=buildingGenerator.count;

        obstacleGenerator.Initialize(carList,busList,carPos);
    }

    // Update is called once per frame
    void Update()
    {
        // set count at each checkpoint
        obstacleGenerator.count=buildingGenerator.count;

        // set position of each generated car
        this.carPos=obstacleGenerator.carPos;
    }
}
