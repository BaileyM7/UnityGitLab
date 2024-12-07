using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class PhoneController : MonoBehaviour
{
    // Audio sources for phone ring and voice talking
    public AudioSource phoneRinging;
    public AudioSource phoneVoice;
    public TMP_Text displayText;

    // The three pairs of order locations and beacons (assign in Inspector)
    public GameObject orderLocation1;
    public GameObject beacon1;
    public GameObject orderLocation2;
    public GameObject beacon2;
    public GameObject orderLocation3;
    public GameObject beacon3;

    // Pizza toppings
    private string[] words = { "cheese", "pepperoni", "sausage" };

    private bool isRinging = false;
    private float ringInterval = 10f; // 5 minutes

    private void Start()
    {
        // Rings every 5 minutes
        InvokeRepeating(nameof(PlayRinging), ringInterval, ringInterval);
    }

    private void PlayRinging()
    {
        if (!isRinging)
        {
            phoneRinging.Play();
            isRinging = true;
            ClearAndDisplayRandomWord(); 
        }
    }

    // Clears the textbox and shows a random word
    private void ClearAndDisplayRandomWord()
    {
        displayText.text = ""; 
        string selectedWord = words[Random.Range(0, words.Length)];
        displayText.text = selectedWord;
    }

    public void OnPhoneClicked()
    {
        if (isRinging)
        {
            phoneRinging.Stop();
            phoneVoice.Play(); 
            isRinging = false;
            displayText.text = "";

            // set the timer for the next ring
            CancelInvoke(nameof(PlayRinging));
            InvokeRepeating(nameof(PlayRinging), ringInterval, ringInterval);

            // toggle a random pair of order location and beacon
            ToggleRandomPair();
        }
    }

private void ToggleRandomPair()
{
    int randomIndex = Random.Range(0, 3); // 0, 1, or 2

    switch (randomIndex)
    {
        case 0:
            TogglePair(orderLocation1, beacon1);
            displayText.fontSize = 1f;
            displayText.text = "Take this pizza to the Drug Store!";
            break;
        case 1:
            TogglePair(orderLocation2, beacon2);
            displayText.fontSize = 1f;
            displayText.text = "Take this pizza to the Construction Site!";
            break;
        case 2:
            TogglePair(orderLocation3, beacon3);
            displayText.fontSize = 1f;
            displayText.text = "Take this pizza to the Theatre!.";
            break;
    }
}


    private void TogglePair(GameObject orderLocation, GameObject beacon)
    {
        bool newActiveState = !orderLocation.activeSelf;
        orderLocation.SetActive(newActiveState);
        beacon.SetActive(newActiveState);
    }
}
