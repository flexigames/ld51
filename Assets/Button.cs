using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour, Interactable
{
    public TextMeshPro displayMesh;
    private float counter = 10 * 1000;

    public void Interact() {
        counter = 10 * 1000;
        UpdateText();
    }

    public void Update() {
        counter -= Time.deltaTime * 1000.0f;
        UpdateText();
    }

    private void UpdateText() {
        displayMesh.text = string.Format("{0:0.00}", counter / 1000f);
    }
}
