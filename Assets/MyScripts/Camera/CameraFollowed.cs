using Cinemachine;
using UnityEngine;

public class CameraFollowed : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float acceleration = 0.06f;
    [SerializeField] private float maxFollowSpeed = 15f;
    void Start()
    {
        offset = transform.position - target.position;
    }
    void LateUpdate()
    {
        if (speed < maxFollowSpeed)
        {
            speed += acceleration;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position + offset, speed * Time.deltaTime);
    }
}