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
            TryInteract();
        }
    }

    void TryInteract() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.0f);

        foreach (Collider collider in colliders) {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null) {
                interactable.Interact();
                break;
            }
        }
    }
}
