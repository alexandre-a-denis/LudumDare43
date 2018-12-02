using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndText : MonoBehaviour
{
    Text text;

    // Use this for initialization
    void Start()
    {
        GetComponent<Text>().text = IAText.Intro();
    }
}
