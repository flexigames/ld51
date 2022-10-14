using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : Interactable
{
    public GameObject[] possibleItems;

    public override void Interact(GameObject holding)
    {
        var randomPrefab = possibleItems[Random.Range(0, possibleItems.Length)];
        Instantiate(randomPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
