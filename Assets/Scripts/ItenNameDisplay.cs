using UnityEngine;
using UnityEngine.UI;

public class ItemNameDisplay : MonoBehaviour
{
    public Text itemNameText;
    public float maxDistance;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Item"))
            {
                ItemComponent itemComponent = hit.collider.GetComponent<ItemComponent>();
                if (itemComponent != null)
                {
                    itemNameText.text = itemComponent.itemData.itemName;
                    itemNameText.enabled = true;
                    
                    Vector3 screenPos = Input.mousePosition;
                    screenPos.y += 30f;
                    itemNameText.rectTransform.position = screenPos;                }
            }
            else
            {
                itemNameText.enabled = false;
            }
        }
        else
        {
            itemNameText.enabled = false;
        }
    }
}