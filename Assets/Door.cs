using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Button
{
    private bool waiting = false;
    public AudioClip doorbellSound;

    public float secondsUntilNextRing;

    public GameObject package;

    void Start()
    {
        base.Start();
        QueueRing();
    }

    private void QueueRing()
    {
        secondsUntilNextRing = Random.Range(2f, 6f);
    }

    public override void Update()
    {
        if (waiting)
        {
            Overlay.Show("Door Counter", counter);
            base.Update();
        }
        else if (secondsUntilNextRing <= 0)
        {
            Debug.Log("Door ringing!");
            waiting = true;
            PlaySound(doorbellSound);
        }
        else
        {
            secondsUntilNextRing -= Time.deltaTime;
            Overlay.Show("Next ring", secondsUntilNextRing);
        }
    }

    public override void OnInteract()
    {
        if (!waiting) return;

        waiting = false;

        DropPackage();
        QueueRing();

    }

    void DropPackage()
    {
        var newPackage = Instantiate(package, package.transform.position, package.transform.rotation);
        newPackage.SetActive(true);
        var rigidBody = newPackage.GetComponent<Rigidbody>();
        rigidBody.AddForce(new Vector3(100f,0f,0f));
    }

    void PlaySound(AudioClip clip) {
        var audio = GetComponent<AudioSource>();
        audio.PlayOneShot(clip);
    }
}
