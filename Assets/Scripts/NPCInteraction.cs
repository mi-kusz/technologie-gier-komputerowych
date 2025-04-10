using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public string npcName = "NPC Janusz";
    public string[] dialogueLines;

    private bool isPlayerLooking = false;
    
    public Transform player;
    public float interactionDistance = 4f;

    void OnMouseEnter()
    {
        isPlayerLooking = true;
    }

    void OnMouseExit()
    {
        isPlayerLooking = false;
    }
    
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(npcName, dialogueLines);
    }
}