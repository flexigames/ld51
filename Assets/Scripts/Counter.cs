using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : Interactable
{
    private float maxTime = 10 * 1000;
    private float counter;

    public bool running = false;

    private float startProbablity = 0.0008f;

    public Vector3 textPosition = new Vector3(0, 2f, 0);

    private GameObject textMesh;

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
        textMesh.transform.localPosition = textPosition;
    }

    void InitializeTextMesh() {
        textMesh = new GameObject("TextMesh");
        textMesh.transform.SetParent(transform, true);
        textMesh.transform.localPosition = textPosition;
        textMesh.AddComponent<TextMeshPro>();
        TextMeshPro textMeshPro = textMesh.GetComponent<TextMeshPro>();
        textMeshPro.text = string.Format("{0:0.00}", counter / 1000f);
        textMeshPro.alignment = TextAlignmentOptions.Center;
        textMeshPro.fontSize = 10f;
        textMeshPro.outlineWidth = 0.2f;
        textMeshPro.outlineColor = Color.black;

        // add outline material
        Material material = new Material(Shader.Find("TextMeshPro/Distance Field"));
        
    }
}
