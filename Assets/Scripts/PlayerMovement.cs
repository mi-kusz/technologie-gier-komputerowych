using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject markerPrefab;
    private GameObject currentMarker;
    private GameObject targetItem;
    private GameObject targetNPC;
    private GameObject targetObstacle;
    
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
                ResetTarget();
                agent.stoppingDistance = 0f;
                agent.SetDestination(hit.point);
                PlaceMarker(hit.point);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    if (currentMarker)
                        Destroy(currentMarker);

                    ResetTarget();
                    targetItem = hit.collider.gameObject;
                    agent.stoppingDistance = 0f;
                    agent.SetDestination(targetItem.transform.position);
                }
                else if (hit.collider.CompareTag("NPC"))
                {
                    if (currentMarker)
                        Destroy(currentMarker);

                    ResetTarget();
                    targetNPC = hit.collider.gameObject;
                    agent.stoppingDistance = 2.5f;
                    agent.SetDestination(targetNPC.transform.position);
                }
                else if (hit.collider.CompareTag("RemovableObstacle"))
                {
                    if (currentMarker)
                        Destroy(currentMarker);

                    ResetTarget();
                    targetObstacle = hit.collider.gameObject;
                    agent.stoppingDistance = 2.5f;
                    agent.SetDestination(targetObstacle.transform.position);
                }
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

                ResetTarget();
            }
            else if (targetNPC)
            {
                float distanceToNPC = Vector3.Distance(transform.position, targetNPC.transform.position);
                
                if (distanceToNPC <= agent.stoppingDistance + 1f)
                {
                    NPCController npcController = targetNPC.GetComponent<NPCController>();
                    if (npcController != null)
                    {
                        npcController.Interact();
                    }
                }
                else
                {
                    Debug.Log("Nie można podejść do NPC.");
                }

                ResetTarget();
            }
            else if (targetObstacle)
            {
                float distanceToObstacle = Vector3.Distance(transform.position, targetObstacle.transform.position);
                
                if (distanceToObstacle <= agent.stoppingDistance + 1f)
                {
                    RemovableObstacle currentObstacleData = targetObstacle.GetComponent<RemovableObstacle>();
                    
                    if (currentObstacleData != null && InventoryManager.Instance.HasItem(currentObstacleData.requiredItemName))
                    {
                        Destroy(targetObstacle);
                        Debug.Log("Przeszkoda usunięta za pomocą: " + currentObstacleData.requiredItemName);
                    }
                    else
                    {
                        Debug.Log("Nie masz przedmiotu: " + (currentObstacleData != null ? currentObstacleData.requiredItemName : "???"));
                    }
                }
                else
                {
                    Debug.Log("Nie można podejść do przeszkody.");
                }
                
                ResetTarget();
            }
            else if (currentMarker)
            {
                Destroy(currentMarker);
            }
        }
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

    private void ResetTarget()
    {
        targetItem = null;
        targetNPC = null;
        targetObstacle = null;
    }
}
