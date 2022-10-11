using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    private CharacterController controller;

    private GameObject holding;

    private float interactDistance = 3.0f;

    public AudioClip pickupSound;
    public AudioClip dropoffSound;
    public AudioClip successSound;
    public AudioClip typingSound;

    public ParticleSystem runningCloud;

    private bool isRunning = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleRunning();

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) {
            OnButtonPress();
        }
    }

    void HandleRunning() {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));   

        if (direction.magnitude < 0.01f) {
            isRunning = false;
            runningCloud.Stop();
            return;
        }

        if (!isRunning) {
            isRunning = true;
            runningCloud.Play();
        }

        direction.Normalize();

        controller.SimpleMove(direction * speed);

        transform.rotation = Quaternion.LookRotation(direction);
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }

    void OnButtonPress() {
        var interacted = TryInteract();

        if (interacted) return;

        if (holding != null) {
            var droppedOff = TryUseDropoff();
            Debug.Log(droppedOff ? "Dropped off somewhere" : "Dropped on floor");
            Drop();
        } else {
            var pickedUp = TryPickup();
            Debug.Log(pickedUp ? "Picked up something" : "Nothing to pick up");
        }
    }

    bool TryUseDropoff() {
        Collider[] colldiers = Physics.OverlapSphere(transform.position, interactDistance);
        foreach (Collider collider in colldiers) {
            DropoffLocation dropoff = collider.GetComponent<DropoffLocation>();
            if (dropoff != null && dropoff.acceptsItemType == holding.GetComponent<PickupItem>().itemType) {
                dropoff.DropOff(holding.gameObject);
                PlaySound(successSound);
                holding = null;
                return true;
            }
        }
        return false;
    }

    bool TryInteract() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactDistance);

        foreach (Collider collider in colliders) {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null) {
                interactable.Interact();
                PlaySound(typingSound);
                return true;
            }
        }

        return false;
    }

    bool TryPickup() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactDistance);

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
        PlaySound(pickupSound);
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
        PlaySound(dropoffSound);

        holding.transform.parent = null;
        holding.GetComponent<Rigidbody>().useGravity = true;
        holding.GetComponent<Rigidbody>().isKinematic = false;

        var force = transform.forward * 5.0f;

        holding.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        holding = null;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * 3.0f;
    }

    void PlaySound(AudioClip clip) {
        var audio = GetComponent<AudioSource>();
        audio.PlayOneShot(clip);
    }
}
