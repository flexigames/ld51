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

    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }
}
