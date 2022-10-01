using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour, Interactable
{
    public TextMeshPro displayMesh;
    private float counter = 10 * 1000;

    public void Interact() {
        counter = 0;
        UpdateText();
    }

    public void Update() {
        counter -= Time.deltaTime * 1000.0f;
        if (counter < 0) {
            counter = 0;
        }

        if (counter == 0) {
            if (Random.value < 0.0008) {
                counter = 10 * 1000;
            }
        }
        UpdateText();
    }

    private void UpdateText() {
        if (counter > 0) {
            displayMesh.text = string.Format("{0:0.00}", counter / 1000f);
        } else {
            displayMesh.text = "";
        }
    }
}
