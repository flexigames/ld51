using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public void Interact() {
        Debug.Log("Door Opened");
    }
}
