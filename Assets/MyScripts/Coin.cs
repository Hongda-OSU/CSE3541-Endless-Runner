using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DetectObstacle())
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(100 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ScoreBoard.numberOfCoins += 1;
            Debug.Log("Coins: " + ScoreBoard.numberOfCoins);
            Destroy(gameObject);
        }
    }

    private bool DetectObstacle()
    {
        bool isDetected = false;

        for (int i = 0; i < 2; ++i)
        {
            var rotation = this.transform.rotation;
            //var rotationMod = Quaternion.AngleAxis(((i / 2.0f)) * 360, this.transform.up);
            var direction = rotation * Vector3.right * 1f;

            var ray = new Ray(this.transform.position + new Vector3(0, -3, 0), direction);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 5f))
            {
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.gameObject.tag == "Car")
                    {
                        Debug.Log("Car detected");
                        Debug.Log(hitInfo.collider.gameObject.transform.position);
                        isDetected = true;
                    }
                }
            }
        }
        return isDetected;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Car")
        {
            Destroy(gameObject);
        }
        Debug.Log(gameObject.name + " in contact with " + collision.collider.name);
    }

    private void OnDrawGizmos()
    {
        //for (int i = 0; i < 2; ++i)
        //{
        //    var rotation = this.transform.rotation;
        //    var rotationMod = Quaternion.AngleAxis(((i / 2.0f)) * 360, this.transform.up);
        //    var direction = rotation * rotationMod * Vector3.forward * 1f;

        //    var ray = new Ray(this.transform.position + new Vector3(0,0,1), direction);
        //    Debug.DrawRay(this.transform.position, direction, Color.black);
        //}

        
            var rotation = this.transform.rotation;
            var direction = rotation * Vector3.right * 5f;

            var ray = new Ray(new Vector3(this.transform.position.x, -3, this.transform.position.z), direction);
            Debug.DrawRay(new Vector3(this.transform.position.x, -3, this.transform.position.z), direction, Color.black);
        
    }
}
