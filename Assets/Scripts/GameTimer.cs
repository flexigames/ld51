using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeSpent = 0;

    void Update()
    {
        timeSpent += Time.deltaTime;
        UpdateText();
    }

    void UpdateText() {
        GetComponent<TextMeshProUGUI>().text = timeSpent.ToString("F2");
    }
}
