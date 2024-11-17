using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearOrder : MonoBehaviour
{
    public Text BearsOrder;

    public void SetText(string text)
    {
        BearsOrder.text = text;
    }
}
