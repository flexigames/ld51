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

    public ParticleSystem runningCloud;

    private Vector3 lastPosition = Vector3.zero;

    private Interactable currentFocus;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleRunning();

        HandleRunningCloud();

        HighlightInteractable();

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) {
            OnButtonPress();
        }

        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space)) {
            if (currentFocus is ContinousInteractable interactable)
            {
                interactable.ContinousInteract();
            }

//            var sinks = FindOfComponentType<Sink>();
//            foreach (var sink in sinks)
//            {
//                sink.InteractLong();
//            }
        }
    }

    Interactable FindInteractable()
    {
        var interactables = FindOfComponentType<Interactable>();

        foreach (var interactable in interactables)
        {
            if (interactable != null && interactable.CanBeUsed(holding)) return interactable;
        }

        return null;
    }

    void HighlightInteractable()
    {
        var interactable = FindInteractable();

        if (interactable == null)
        {
            if (currentFocus != null)
            {
                currentFocus.UnFocus();
                currentFocus = null;
            }
        } else if (interactable != currentFocus)
        {
            if (currentFocus != null)
            {
                currentFocus.UnFocus();
            }
            interactable.Focus();
            currentFocus = interactable;
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
        if (!interacted && holding)
        {
            Drop();
        }
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
        if (currentFocus == null) return false;

        currentFocus.Interact(holding);

        if (currentFocus is PickupItem pickupItem)
        {
            Pickup(pickupItem.gameObject);
        }
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
