using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject leftBuiilding;
    public GameObject rightBuilding;
    public GameObject path;
    public GameObject character;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // detech if player has reached position for spawning next tiles
        if (character.transform.position.z > count * 100 + 30)
        {
            // increment counter until player gets to next position to spawn
            count++;

            // clone left and right buildings
            Instantiate(leftBuiilding, new Vector3(leftBuiilding.transform.position.x, 0,count * 120), leftBuiilding.transform.rotation);
            Instantiate(rightBuilding, new Vector3(rightBuilding.transform.position.x, 0, count * 120), rightBuilding.transform.rotation);

            // clone path
            if (count >= 2)
            {
                Instantiate(path, new Vector3(path.transform.position.x, 0, (count-1) * 110 + 150), Quaternion.identity);
            }
            else
            {
                Instantiate(path, new Vector3(path.transform.position.x, 0, count * 150), Quaternion.identity);
            }

        }
    }
}
