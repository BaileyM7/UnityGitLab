using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    public TMP_Text[] textList;
    private bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked() {
        active = !active;
        foreach(TMP_Text text in textList) {
            text.enabled = active;
        }
    }
}
