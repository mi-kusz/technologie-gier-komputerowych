using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameObject player;
    public GameObject endMessageUI;
    private bool gameEnded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            endMessageUI.SetActive(true);
            gameEnded = true;
        }
    }

    void Update()
    {
        if (gameEnded && Input.GetKeyDown(KeyCode.Space))
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                        Application.Quit();
            #endif
        }
    }
}