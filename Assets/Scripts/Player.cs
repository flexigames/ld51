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

    private Vector3 lastPosition = Vector3.zero;

    private PickupItem currentFocus;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleRunning();

        HandleRunningCloud();

        FindInteractable();

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) {
            OnButtonPress();
        }

        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space)) {
            var sinks = FindOfComponentType<Sink>();
            foreach (var sink in sinks)
            {
                sink.Interact();
            }
        }
    }

    void FindInteractable()
    {
        // TODO: I still need to make sure picking up and interacting is the same thing

        var pickup = FindPickup();

        if (pickup == null)
        {
            if (currentFocus != null)
            {
                currentFocus.UnFocus();
                currentFocus = null;
            }
        } else if (pickup != currentFocus)
        {
            if (currentFocus != null)
            {
                currentFocus.UnFocus();
            }
            pickup.Focus();
            currentFocus = pickup;
        }
    }

    void HandleRunningCloud() {
        if (lastPosition != transform.position) {
            if (!runningCloud.isEmitting) {
                runningCloud.Play();
            }
            lastPosition = transform.position;
        } else {
            if (runningCloud.isEmitting) {
                runningCloud.Stop();
            }
        }
    }

    void HandleRunning() {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));   

        if (direction.magnitude < 0.01f) return;

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
        var dropOffs = FindOfComponentType<DropoffLocation>();
        foreach (var dropoff in dropOffs) {
            if (dropoff.acceptsItemType == holding.GetComponent<PickupItem>().itemType) {
                dropoff.DropOff(holding.gameObject);
                PlaySound(successSound);
                holding = null;
                return true;
            }
        }
        return false;
    }

    List<T> FindOfComponentType<T>()
    {
        Collider[] colldiers = Physics.OverlapSphere(transform.position, interactDistance);
        var listOfComponentTypes = new List<T>();
        foreach (Collider collider in colldiers) {
            T componentType = collider.GetComponent<T>();
            if (componentType != null) listOfComponentTypes.Add(componentType);
        }
        return listOfComponentTypes;
    }

    T FindOneOfComponentType<T>()
    {
        List<T> componentTypes = FindOfComponentType<T>();

        if (componentTypes.Count > 0) return componentTypes[0];

        return default(T);
    }

    bool TryInteract() {
        var interactable = FindOneOfComponentType<Interactable>();

        if (interactable == null) return false;

        interactable.Interact();
        PlaySound(typingSound);
        return true;
    }

    PickupItem FindPickup()
    {
        var pickupItems = FindOfComponentType<PickupItem>();

        foreach (var pickupItem in pickupItems) {
            if (holding != null && pickupItem.gameObject == holding) {
                continue;
            }

            return pickupItem;
        }

        return null;
    }

    bool TryPickup() {
        var pickupItem = FindPickup();

        if (pickupItem == null) return false;

        Pickup(pickupItem.gameObject);
        return true;
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
