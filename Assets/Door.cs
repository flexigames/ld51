using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Button
{
    private bool waiting = false;
    public AudioClip doorbellSound;

    public float secondsUntilNextRing;

    void Start()
    {
        base.Start();
        QueueRing();
    }

    private void QueueRing()
    {
        secondsUntilNextRing = Random.Range(3f, 10f);
    }

    public override void Update()
    {
        if (waiting)
        {
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
        }
    }

    public override void OnInteract()
    {
        waiting = false;
        QueueRing();
    }

    void PlaySound(AudioClip clip) {
        var audio = GetComponent<AudioSource>();
        audio.PlayOneShot(clip);
    }
}
