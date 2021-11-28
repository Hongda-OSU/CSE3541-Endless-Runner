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
        leftBuiilding.name="LeftBuilding count:"+count;
        rightBuilding.name="RightBuilding count:"+count;
        path.name="Path count:"+count;
    }

    // Update is called once per frame
    void Update()
    {
        // detech if player has reached position for spawning next tiles
        if (character.transform.position.z > count * 120 + 30)
        {
            // increment counter until player gets to next position to spawn
            count++;

            // clone left and right buildings
            var newLeftBuilding=Instantiate(leftBuiilding, new Vector3(leftBuiilding.transform.position.x, 0,count * 120), leftBuiilding.transform.rotation);
            var newRightBuilding=Instantiate(rightBuilding, new Vector3(rightBuilding.transform.position.x, 0, count * 120), rightBuilding.transform.rotation);

            // change name of new clone
            newLeftBuilding.name="LeftBuilding count:"+count;
            newRightBuilding.name="RightBuilding count:"+count;
          
            //if (count >= 2)
            {
                // clone path
                // var newPath=Instantiate(path, new Vector3(path.transform.position.x, 0, (count-1) * 110 + 150), path.transform.rotation);
                var newPath=Instantiate(path, new Vector3(path.transform.position.x, 0, (count)*120+50), path.transform.rotation);

                // change name of new path
                newPath.name="Path count:"+count;

                // destory old path and building
                GameObject leftBuildingToDestroy=GameObject.Find("LeftBuilding count:"+(count-2));
                GameObject rightBuildingToDestroy=GameObject.Find("RightBuilding count:"+(count-2));
                GameObject pathToDestroy=GameObject.Find("Path count:"+(count-2));
                Destroy(leftBuildingToDestroy);
                Destroy(rightBuildingToDestroy);
                Destroy(pathToDestroy);

                // set new building and path to clone 
                leftBuiilding=GameObject.Find("LeftBuilding count:"+(count-1));
                rightBuilding=GameObject.Find("RightBuilding count:"+(count-1));
                path=GameObject.Find("Path count:"+(count-1));
            }
            // else
            // {
            //     var newPath=Instantiate(path, new Vector3(path.transform.position.x, 0, count * 150), Quaternion.identity);
            //     newPath.name="Path count:"+count;
            // }
        }
    }
}
