using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    private CharacterController controller;

    private GameObject holding;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));   

        if (direction.magnitude > 0.01f) {
            direction.Normalize();
            
            controller.Move(direction * speed * Time.deltaTime);


            transform.rotation = Quaternion.LookRotation(direction);
        }
    
        if (Input.GetKeyDown(KeyCode.Space)) {
            TryInteract();
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (holding != null) {
                TryUseDropoff();
                Drop();
            } else {
                TryPickup();
            }
        }
    }

    bool TryUseDropoff() {
        Collider[] colldiers = Physics.OverlapSphere(transform.position, 2.0f);
        foreach (Collider collider in colldiers) {
            DropoffLocation dropoff = collider.GetComponent<DropoffLocation>();
            if (dropoff != null && dropoff.acceptsItemType == holding.GetComponent<PickupItem>().itemType) {
                Destroy(holding.gameObject);
                holding = null;
                return true;
            }
        }
        return false;
    }

    bool TryInteract() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.0f);

        foreach (Collider collider in colliders) {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null) {
                interactable.Interact();
                return true;
            }
        }

        return false;
    }

    bool TryPickup() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.0f);

        foreach (Collider collider in colliders) {
            PickupItem pickupItem = collider.GetComponent<PickupItem>();
            if (pickupItem != null) {
                if (holding != null && pickupItem.gameObject == holding) {
                    continue;
                }

                Pickup(pickupItem.gameObject);
                return true;
            }
        }
        return false;
    }

    void Pickup(GameObject pickupItem) {    
        if (holding != null) {
            Drop();
        }
        pickupItem.transform.parent = transform;
        pickupItem.transform.localPosition = new Vector3(0, 1.5f, 1.5f);
        pickupItem.transform.localRotation = Quaternion.identity;

        pickupItem.GetComponent<Rigidbody>().useGravity = false;
        pickupItem.GetComponent<Rigidbody>().isKinematic = true;

        holding = pickupItem;
    }

    void Drop() {
        if (holding == null) {
            return;
        }
        holding.transform.parent = null;
        holding.GetComponent<Rigidbody>().useGravity = true;
        holding.GetComponent<Rigidbody>().isKinematic = false;

        holding.GetComponent<Rigidbody>().AddForce(transform.forward * 3.0f, ForceMode.Impulse);

        holding = null;
    }
}
