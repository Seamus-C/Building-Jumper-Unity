using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPadController : MonoBehaviour {

    [SerializeField] private Animator myLanchPad = null;

    [SerializeField] private bool launchTrigger = false;
    [SerializeField] private bool unlaunchTrigger = false;


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {

            if (launchTrigger) {

                myLanchPad.Play("Launch", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }

    }
}