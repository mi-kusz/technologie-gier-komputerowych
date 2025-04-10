using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Ogólne")]
    public string npcName;
    public Transform player;
    public float interactionDistance = 4f;

    [Header("Dialog zwykły")]
    public string[] defaultDialogue;

    [Header("Quest")]
    public bool hasQuest = false;
    public string questName;
    public string[] dialogueBeforeQuest;
    public string[] dialogueQuestIncomplete;
    public string[] dialogueQuestComplete;

    public string requiredItemName;
    public int requiredItemCount;
    public ItemData rewardItem;

    private bool questGiven = false;

    void OnMouseOver()
    {
    }

    public void Interact()
    {
        if (!hasQuest)
        {
            // zwykły NPC
            DialogueManager.Instance.StartDialogue(npcName, defaultDialogue);
            return;
        }

        var qm = QuestManager.Instance;

        if (!questGiven && !qm.GetQuests().Exists(q => q.questName == questName))
        {
            // Daj questa
            DialogueManager.Instance.StartDialogue(npcName, dialogueBeforeQuest);
            qm.AddQuest(new QuestData(questName));
            questGiven = true;
        }
        else if (!qm.GetQuests().Find(q => q.questName == questName).isCompleted)
        {
            // Sprawdź przedmioty
            int count = InventoryManager.Instance.CountItem(requiredItemName);
            if (count >= requiredItemCount)
            {
                InventoryManager.Instance.RemoveItem(requiredItemName, requiredItemCount);
                InventoryManager.Instance.AddItem(rewardItem);

                qm.CompleteQuest(questName);
                print("X");
                DialogueManager.Instance.StartDialogue(npcName, dialogueQuestComplete);
            }
            else
            {
                DialogueManager.Instance.StartDialogue(npcName, dialogueQuestIncomplete);
            }
        }
        else
        {
            // Quest już ukończony
            print("Y");
            DialogueManager.Instance.StartDialogue(npcName, dialogueQuestComplete);
        }
    }
}
