using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public GameObject container;
    public GameObject bar;

    private float lastProgress;

    public static ProgressBar Add(Transform parent)
    {
        var progressBarGameObject = Instantiate(Resources.Load("ProgressBar", typeof(GameObject)), parent) as GameObject;
        return progressBarGameObject.GetComponent<ProgressBar>();
    }

    void Update()
    {
        container.SetActive(Time.time - lastProgress < 0.2);
    }

    public void SetProgress(float value)
    {
        bar.transform.localScale = new Vector3(value, 1, 1);
        lastProgress = Time.time;
    }
}
