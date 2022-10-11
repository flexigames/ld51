using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Button : Interactable
{
    public TextMeshPro displayMesh;
    private float counter;

    public float maxCounter = 11 * 1000;

    private float maxTimeShown = 10 * 1000;

    void Start() {
        counter = maxCounter;
    }

    public override void Interact() {
        counter = maxCounter;
        UpdateText();
    }

    public void Update() {
        counter -= Time.deltaTime * 1000.0f;

        if (counter < 0) {
            SceneManager.LoadScene("GameOver");
        }
        UpdateText();
    }

    private void UpdateText() {
        if (counter < maxTimeShown) {
            displayMesh.text = string.Format("{0:0.00}", counter / 1000f);
        } else {
            displayMesh.text = string.Format("{0:0.00}", maxTimeShown / 1000f);
        }
    }
}