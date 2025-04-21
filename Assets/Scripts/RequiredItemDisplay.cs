using UnityEngine;
using UnityEngine.UI;

public class RequiredItemDisplay : MonoBehaviour
{
    public Text requiredItemText;
    public float maxDistance;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("RemovableObstacle"))
            {
                RemovableObstacle removableObstacle = hit.collider.GetComponent<RemovableObstacle>();
                if (removableObstacle != null)
                {
                    requiredItemText.text = "Wymagany przedmiot: " + removableObstacle.requiredItemName;
                    requiredItemText.enabled = true;
                    
                    Vector3 screenPos = Input.mousePosition;
                    screenPos.y += 30f;
                    requiredItemText.rectTransform.position = screenPos;
                }
            }
            else
            {
                requiredItemText.enabled = false;
            }
        }
        else
        {
            requiredItemText.enabled = false;
        }
    }
}
