using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPadController : MonoBehaviour
{

    [SerializeField] private Animator animator;

    [SerializeField] private string animationName = "Launch";

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Is tigering!");
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.Play(animationName, 0, 0f);
        }
    }
}

   /*private void OnTriggerEnter(Collider other) {
        Debug.Log("Is tigering!");
        if (other.CompareTag("Player")) {
            animator.Play(animationName, 0, 0f);
        }
    }
   
}
*/