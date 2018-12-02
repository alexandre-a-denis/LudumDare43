using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    public Text status;
    public Text cause;

    public void SetStatusText(string txt)
    {
        status.text = txt;
    }

    public void SetCauseText(string txt)
    {
        cause.text = txt;
    }
}
