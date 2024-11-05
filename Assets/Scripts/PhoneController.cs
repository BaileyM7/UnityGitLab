using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; 

public class PhoneController : MonoBehaviour
{
    public AudioSource phoneRinging;
    public TMP_Text displayText;
    private bool isRinging = false;
    private string[] words = { "cheese", "pepperoni", "sausage" }; // possible pizza selection

    private void Start()
    {
        InvokeRepeating(nameof(PlayRinging), 60f, 60f); // ring every 60 seconds
    }

    private void PlayRinging()
    {
        if (!isRinging)
        {
            phoneRinging.Play();
            isRinging = true;
            ClearAndDisplayRandomWord(); // clear textbox and display a random word on each ring
        }
    }

    private void ClearAndDisplayRandomWord()
    {
        displayText.text = ""; 
        string selectedWord = words[Random.Range(0, words.Length)]; // select cheese or pepperoni 
        displayText.text = selectedWord;
    }

    public void OnPhoneClicked()
    {
        if (isRinging)
        {
            phoneRinging.Stop();
            isRinging = false;
            displayText.text = "";
        }
    }
}
