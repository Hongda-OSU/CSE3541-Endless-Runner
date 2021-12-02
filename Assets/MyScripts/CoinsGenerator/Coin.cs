using UnityEngine;

public class Coin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(100 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MenuManager.coinCount += 1;
            Destroy(gameObject);
        }
    }
}
