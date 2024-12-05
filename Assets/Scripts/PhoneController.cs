using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class PhoneController : MonoBehaviour
{
    // audio sources for phone ring and voice talking
    public AudioSource phoneRinging;
    public AudioSource phoneVoice;
    public TMP_Text displayText;
    private bool isRinging = false;
    // current options for pizza toppings
    private string[] words = { "cheese", "pepperoni", "sausage" };



    private float ringInterval = 300f; // this is five minutes

    private void Start()
    {
        // rings every 5 minutes
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

    // clears the textbox and shows a random word
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
            // played the voice on the phone audio on clikc
            phoneVoice.Play(); 
            isRinging = false;
            displayText.text = "";

            // resstarts the timer
            CancelInvoke(nameof(PlayRinging));
            InvokeRepeating(nameof(PlayRinging), ringInterval, ringInterval);
        }
    }
}
