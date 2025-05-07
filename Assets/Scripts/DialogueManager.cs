using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialogueUI;
    public Text npcNameText;
    public Text dialogueText;

    private int currentLineIndex;
    private string[] currentLines;
    private bool isDialogueActive = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialogueUI.SetActive(false);
    }

    public void StartDialogue(string npcName, string[] lines)
    {
        currentLines = lines;
        currentLineIndex = 0;
        npcNameText.text = npcName;
        isDialogueActive = true;
        dialogueUI.SetActive(true);
        ShowLine();
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            ShowLine();
        }
    }

    private void ShowLine()
    {
        if (currentLineIndex < currentLines.Length)
        {
            dialogueText.text = currentLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false);
    }
}