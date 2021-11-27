using UnityEngine;

public class CameraFixed : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    private Vector3 originalTrans;
    void Start()
    {
        offset = transform.position - target.position;
        originalTrans = transform.position;
    }
    void LateUpdate()
    {
        Vector3 desirePosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        if (target.position.y > 0)
        {
            desirePosition.y = offset.y + target.position.y;
        }
        else
        {
            desirePosition.y = originalTrans.y;
        }
        transform.position = Vector3.Lerp(transform.position, desirePosition, 0.6f);
    }
}