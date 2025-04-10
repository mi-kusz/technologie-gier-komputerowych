using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private List<QuestData> quests = new List<QuestData>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddQuest(QuestData quest)
    {
        quests.Add(quest);
        Debug.Log("Dodano questa: " + quest.questName);
        FindObjectOfType<QuestUI>().UpdateQuestUI();
    }

    public void CompleteQuest(string questName)
    {
        QuestData quest = quests.Find(q => q.questName == questName);
        if (quest != null)
        {
            quest.isCompleted = true;
            Debug.Log("Ukończono questa: " + quest.questName);
            FindObjectOfType<QuestUI>().UpdateQuestUI();
        }
    }

    public List<QuestData> GetQuests()
    {
        return quests;
    }
}