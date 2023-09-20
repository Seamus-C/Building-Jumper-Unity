using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPadController : MonoBehaviour
{
    
    [SerializeField] private Animator myLanchPad = null;

    public Animation animation; 
    /*
    public KeyCode jumpKey = KeyCode.Space;

    public bool readyToLaunch;

    private void Update()
    {
        readyToLaunch = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsLaunch);
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerMovement.Launch)
        {
            myLanchPad.
        }

    }

     if (collision.gameObject.CompareTag("LaunchPad"))
        {
            Launch();
}
}    