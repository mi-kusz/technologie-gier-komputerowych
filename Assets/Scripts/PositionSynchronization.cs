using UnityEngine;

public class PositionSynchronization : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    
    void Start()
    {
        transform.position = target.position + offset;
        Debug.Log("Wyrównano pozycje podłoża");
    } 
}
