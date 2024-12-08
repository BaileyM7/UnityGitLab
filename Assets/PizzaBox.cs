using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBox : MonoBehaviour
{
    private bool opened = false;
    public Animator anim;

    // 1 - Close
    // 2 - Open
    public void onToggle() {
        opened = !opened;
        if (opened) {
            anim.Play("PizzaBoxOpen");
        } else {
            anim.Play("PizzaBoxClose");
        }
    }
}
