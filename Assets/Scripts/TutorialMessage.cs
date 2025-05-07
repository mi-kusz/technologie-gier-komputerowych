using UnityEngine;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour
{
    public GameObject messagePanel;
    [TextArea]
    public string tutorialMessage;
    public GameObject player;

    private bool isInTriggerZone = false;
    private bool isMessageActive = false;
    private bool messageDisplayed = false;

    void Update()
    {
        if (isInTriggerZone && !messageDisplayed)
        {
            ShowTutorialMessage();
        }

        if (isMessageActive && Input.GetKeyDown(KeyCode.Space))
        {
            HideTutorialMessage();
        }
    }

    private void ShowTutorialMessage()
    {
        Time.timeScale = 0;
        messagePanel.SetActive(true);
        messagePanel.GetComponentInChildren<Text>().text = tutorialMessage;
        isMessageActive = true;
        messageDisplayed = true;
    }

    private void HideTutorialMessage()
    {
        Time.timeScale = 1;
        messagePanel.SetActive(false);
        isMessageActive = false;
        isInTriggerZone = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isInTriggerZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isInTriggerZone = false;
        }
    }
}