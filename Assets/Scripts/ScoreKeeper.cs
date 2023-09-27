using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**************************************
 * attached to player: increases point count on player collision with each platform for the first time.
 * 
 * Bryce Haddock 1.0 September 22, 2023
 * ************************************/
public class Scorekeeper : MonoBehaviour
{

    [SerializeField] private int scoreAdded = 1;
    [SerializeField] private TextMeshProUGUI scoreboardText;
    public float score;
    private HashSet<Collider> collidedPlatforms = new HashSet<Collider>(); // To track collided platforms

    public static Scorekeeper Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    // Adds to score on initial player collision trigger with a platform 
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with platform."); // Add this line for debugging

        // Check if the collision is with a platform and if it's the first time
        if (collision.gameObject.CompareTag("Platform") && !collidedPlatforms.Contains(collision.collider))
        {
            collidedPlatforms.Add(collision.collider); // Mark the platform as collided

            // Increase the score (you can adjust this as needed)
            score += scoreAdded; // Example: Add # points for each platform collision

            // Update the UI
            DisplayScore();
        }
    } 
         
    
    //Displays score to UI rounded to nearest integer
    public void DisplayScore()
    {
           // Round the score to the nearest integer
        int roundedScore = Mathf.RoundToInt(score);
        
        // Update the TextMeshProUGUI text component to display the score
        scoreboardText.text = "Score: " + roundedScore.ToString();                              
    }
}

    