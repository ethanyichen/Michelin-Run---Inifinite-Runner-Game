﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text321goScript : MonoBehaviour {

    // text Variables
    public Text Number3;
    public Text Number2;
    public Text Number1;
    public Text Go;
    public float Timeleft = 3f;

    void DisplayText(Text text)
    {
        text.enabled = true;
    }
    void DisableText(Text text)
    {
        text.enabled = false;
    }
    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count >= 0)
        {
            if (count == 3)
            {
                DisplayText(Number3);
            }
            if (count == 2)
            {
                DisableText(Number3);
                DisplayText(Number2);
            }
            if (count == 1)
            {
                DisableText(Number2);
                DisplayText(Number1);
            }
            if (count == 0)
            {
                DisableText(Number1);
                DisplayText(Go);
            }
            yield return new WaitForSeconds(1);
            count--;
        }
        DisableText(Go);
    }

    void Start()
    {
        StartCoroutine(Countdown(3));
    }


}