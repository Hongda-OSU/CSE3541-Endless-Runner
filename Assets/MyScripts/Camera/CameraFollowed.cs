using UnityEngine;

public class CameraFollowed : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;
    }
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + offset, 10f * Time.smoothDeltaTime);
    }
}