using UnityEngine;
using UnityEngine.AI;

public class ChangeWorld : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    public GameObject firstWorld;
    private Collider _firstSurfaceCollider;
    
    public GameObject secondWorld;
    private Collider _secondSurfaceCollider;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _firstSurfaceCollider = firstWorld.GetComponent<Collider>();
        _secondSurfaceCollider = secondWorld.GetComponent<Collider>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsAgentOnSurface(_firstSurfaceCollider))
            {
                TeleportToSurface(_secondSurfaceCollider);
                Debug.Log("Przeniesiono na drugie podłoże");
            }
            else
            {
                TeleportToSurface(_firstSurfaceCollider);
                Debug.Log("Przeniesiono na pierwsze podłoże");
            }
        }
    }
    
    bool IsAgentOnSurface(Collider surfaceCollider)
    {
        Vector3 closestPoint = surfaceCollider.ClosestPoint(_agent.transform.position);

        return Vector3.Distance(_agent.transform.position, closestPoint) < 2f;
    }
    
    void TeleportToSurface(Collider collider)
    {
        Vector3 closestPoint = collider.ClosestPoint(_agent.transform.position);
        
        Vector3 newPosition = new Vector3(_agent.transform.position.x, closestPoint.y, _agent.transform.position.z);
        
        _agent.Warp(newPosition);
    }
}