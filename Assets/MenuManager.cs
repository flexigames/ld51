using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject startButton;

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.gameObject == startButton) {
                if (Input.GetMouseButtonDown(0)) {
                    SceneManager.LoadScene("Scene", LoadSceneMode.Single);
                }
            }
        }
    }

}
