using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public bool handleGameOver = true;

    private static Game instance;

    public static void OnGameOver()
    {
        if (!instance.handleGameOver) return;
        SceneManager.LoadScene("GameOver");
    }

    public static void IncreaseSteps(string taskId)
    {
        TaskManager taskManager = FindObjectOfType<TaskManager>();
        taskManager.IncreaseSteps(taskId);
    }

    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }
}
