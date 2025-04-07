using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject markerPrefab;
    private GameObject currentMarker;
    private GameObject targetItem;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) &&  hit.collider.tag == "Ground")
            {
                targetItem = null;
                agent.SetDestination(hit.point);
                PlaceMarker(hit.point);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag("Item"))
            {
                if (currentMarker)
                    Destroy(currentMarker);
                
                targetItem = hit.collider.gameObject;
                agent.SetDestination(targetItem.transform.position);
            }
        }
        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (targetItem)
            {
                float distanceToItem = Vector3.Distance(transform.position, targetItem.transform.position);

                if ((agent.hasPath || agent.remainingDistance <= agent.stoppingDistance) && distanceToItem <= 2f)                {
                    TryPickUp(targetItem);
                }
                else
                {
                    Debug.Log("Nie można dotrzeć do przedmiotu.");
                }

                targetItem = null;
            }
            else if (currentMarker)
            {
                Destroy(currentMarker);
            }
        }
        
        /*
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (targetItem != null)
            {
                TryPickUp(targetItem);
                targetItem = null;
            }
            else if (currentMarker)
            {
                Destroy(currentMarker);
            }
        }
        */
        
        /*if (currentMarker && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Destroy(currentMarker);
        }
        */
    }
    
    void PlaceMarker(Vector3 position)
    {
        if (currentMarker)
            Destroy(currentMarker);
        
        currentMarker = Instantiate(markerPrefab, position, Quaternion.identity);
    }
    
    void TryPickUp(GameObject itemObject)
    {
        ItemComponent itemComponent = itemObject.GetComponent<ItemComponent>();
        if (itemComponent)
        {
            InventoryManager.Instance.AddItem(itemComponent.itemData);
            Destroy(itemObject);
        }
    }
}
