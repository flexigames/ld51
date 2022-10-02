using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : Interactable
{
    private float maxTime = 10 * 1000;
    private float counter;

    private bool running = false;

    private float startProbablity = 0.0008f;

    void Start()
    {  
        InitializeTextMesh();
    }

    public override void Interact()
    {
        running = false;
        counter = 0;
        UpdateText();
    }

    void Update() {
        if (running) {
            counter -= Time.deltaTime * 1000.0f;
            if (counter < 0) {
                counter = 0;
                running = false;
            }
        }

        if (counter == 0) {
            if (Random.value < startProbablity) {
                counter = maxTime;
                running = true;
            }
        }

        UpdateText();
    }

    void UpdateText() {
        if (counter > 0) {
            transform.Find("TextMesh").GetComponent<TextMeshPro>().text = string.Format("{0:0.00}", counter / 1000f);
        } else {
            transform.Find("TextMesh").GetComponent<TextMeshPro>().text = "";
        }
    }

    void InitializeTextMesh() {
        GameObject textMesh = new GameObject("TextMesh");
        textMesh.transform.SetParent(transform, true);
        textMesh.transform.localPosition = new Vector3(0, 1.5f, 0);
        textMesh.AddComponent<TextMeshPro>();
        textMesh.GetComponent<TextMeshPro>().text = string.Format("{0:0.00}", counter / 1000f);
        textMesh.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
        textMesh.GetComponent<TextMeshPro>().fontSize = 10f;
    }
}
