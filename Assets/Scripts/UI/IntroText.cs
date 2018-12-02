using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroText : MonoBehaviour
{
    Text text;

    // Use this for initialization
    void Start()
    {
        GetComponent<Text>().text = IAText.Intro() + "\n\n\n\n\n" + IAText.Help();
    }
}
