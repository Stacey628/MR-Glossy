using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HUDTextUpdater : MonoBehaviour
{
    private TextMeshProUGUI hudText;

    // Variables to track points and time
    private int points = 0;          // Start points at zero
    private int elapsedTime = 0;     // Time in seconds, starting at zero

    void Start()
    {
        // Reference the TextMeshPro component
        hudText = GetComponent<TextMeshProUGUI>();

        // Set the initial HUD text
        UpdateHUDText();

        // Start the stopwatch coroutine
        StartCoroutine(UpdateStopwatch());

        // Start the random points increment coroutine
        //StartCoroutine(RandomlyIncreasePoints());
    }

    // Method to update the HUD text with current points and elapsed time
    private void UpdateHUDText()
    {
        hudText.text = $"\n{points} Points\n{elapsedTime} Seconds";
    }

    // Coroutine to increment the stopwatch every second
    private IEnumerator UpdateStopwatch()
    {
        while (true) // Infinite loop to keep the stopwatch running
        {
            yield return new WaitForSeconds(1); // Wait for 1 second
            elapsedTime++; // Increment the timer
            UpdateHUDText(); // Update the text with the new time
        }
    }

    // Public method to increase points by a specific amount
	public void AddPoints(int amount)
	{
	    points += amount;
	    UpdateHUDText(); // Update the HUD with the new points
	    
	    // Check if points equals 5
	    if (points == 5)
	    {
		// Start coroutine to load end scene after 4 seconds
		StartCoroutine(WaitAndLoadEndScene());
	    }
	}
    
    // Coroutine to wait for 4 seconds before loading the end scene
	private IEnumerator WaitAndLoadEndScene()
	{
	    yield return new WaitForSeconds(3f); // Wait for n seconds
	    LoadEndScene(); // Call the method to load the end scene
	}
    
    public void LoadEndScene()
    {
        SceneManager.LoadScene("END_SCREEN");
    }

    // Coroutine to increase points randomly every 2 to 6 seconds
    private IEnumerator RandomlyIncreasePoints()
    {
        while (true) // Infinite loop to keep incrementing points
        {
            int randomInterval = Random.Range(2, 7); // Random number between 2 and 6 seconds
            yield return new WaitForSeconds(randomInterval); // Wait for the random interval
            AddPoints(1); // Add 1 point
        }
    }
}


