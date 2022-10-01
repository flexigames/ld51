using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));   

        if (direction.magnitude > 0.3) {
            direction.Normalize();
            
            controller.Move(direction * speed * Time.deltaTime);


            transform.rotation = Quaternion.LookRotation(direction);
        }
    
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");

            GameObject closest = null;
            float closestDistance = Mathf.Infinity;
            foreach (GameObject interactable in interactables) {
                float distance = Vector3.Distance(transform.position, interactable.transform.position);
                if (distance < closestDistance) {
                    closest = interactable;
                    closestDistance = distance;
                }
            }

            if (closest != null && closestDistance < 3) {
                closest.GetComponent<Interactable>().Interact();
            }
        }
    }
}
