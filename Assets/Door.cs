using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour, Interactable
{

    public void Interact() {
        Debug.Log("Door Opened");
    }
}
