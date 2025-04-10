using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestUI : MonoBehaviour
{
    public Text questText;
    public string head;

    public void UpdateQuestUI()
    {
        List<QuestData> quests = QuestManager.Instance.GetQuests();
        questText.text = head + "\n";

        foreach (QuestData quest in quests)
        {
            if (quest.isCompleted)
                continue;
            questText.text += quest.questName + "\n";
        }
    }

    void Start()
    {
        QuestManager.Instance.AddQuest(new QuestData("Znajdź starożytny miecz"));
        QuestManager.Instance.AddQuest(new QuestData("Zanieś list do kapłanki"));
        UpdateQuestUI();
    }
}