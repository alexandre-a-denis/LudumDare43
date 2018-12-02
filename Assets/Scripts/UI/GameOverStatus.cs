using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverStatus : MonoBehaviour
{

    bool endDisplayed = false;
    WorldState state;
    Text text;
    private Color color = Color.red;
    public float fadeSpeed = 6.5f;

    // Use this for initialization
    void Start()
    {
        state = FindObjectOfType<WorldState>();
        text = GetComponent<Text>();
        text.color = new Color(0,0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.manager.End)
        {
            if (!endDisplayed)
            {
                endDisplayed = true;
                StartCoroutine("Fade");
            }
        }
    }

    IEnumerator Fade()
    {
        float step = 0;
        while (true)
        {
            step += fadeSpeed * Time.deltaTime;
            text.color = new Color(color.r, color.g, color.b, (1 + Mathf.Sin(step)) / 2f);
            yield return null;
        }
    }

}
