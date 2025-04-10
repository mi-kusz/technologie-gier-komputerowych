[System.Serializable]
public class QuestData
{
    public string questName;
    public bool isCompleted;

    public QuestData(string name, bool completed = false)
    {
        questName = name;
        isCompleted = completed;
    }
}