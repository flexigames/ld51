using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, Interactable
{
    public void Interact() {
        Debug.Log("Button Pressed");
    }
}
