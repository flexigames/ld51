using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    private string templateText;

    void Start() {
        templateText = GetComponent<TextMeshPro>().text;
        UpdateText();
    }

    void UpdateText() {
        GetComponent<TextMeshPro>().text = templateText.Replace("%time", GlobalState.time.ToString("F2"));
    }
}
