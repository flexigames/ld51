using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Overlay : MonoBehaviour
{
    public TextMeshProUGUI text;

    private static Overlay instance;

    private static Dictionary<string, string> values = new Dictionary<string, string>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    private void LateUpdate()
    {
        instance.text.text = "";

        foreach(KeyValuePair<string, string> data in values)
        {
            instance.text.text += data.Key + ": " + data.Value + "\n";
        }
    }

    public static void Show<T>(string name, T message)
    {
        values[name] = message.ToString();
    }
}
