using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    void Update()
    {
        transform.Rotate( 0, 100 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (AudioManager.instance != null)
            {
                FindObjectOfType<AudioManager>().Play("Collect");
            }
            MenuManager.collectionCount += 1;
            Destroy(gameObject);
        }
    }
}