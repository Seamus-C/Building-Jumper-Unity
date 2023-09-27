using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkManager : MonoBehaviour {

    [SerializeField] private GameObject[] firework;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            // player in collider

            foreach (GameObject gameObject in firework) {
                gameObject.SetActive(true);

                Debug.Log("firework lauch!");
            }
        }
    }
}