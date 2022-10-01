using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDetector : MonoBehaviour
{
    private List<GameObject> currentCollisions = new List<GameObject>();

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (GameObject obj in currentCollisions) {
                Debug.Log(obj.name);
            }
        }
    }

    void OnTriggerEnter (Collider col) {
        Debug.Log("Collision Enter");
        currentCollisions.Add(col.gameObject);
     }
 
     void OnTriggerExit (Collider col) {
         currentCollisions.Remove(col.gameObject);
     }
}
