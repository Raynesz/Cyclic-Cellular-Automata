using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Options : MonoBehaviour
{
    public int output=18;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleInputData(int val) {
        if (val == 0) output = 18;
        else if (val == 1) output = 12;
        else output = 6;
    }

    public void SwapButtonText()
    {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        GameObject startPauseButton = GameObject.Find("Start/Pause (TMP)");
        if (texture.GetComponent<Texture>().enabled)
        {
            startPauseButton.GetComponent<TMP_Text>().text = "Pause";
        }
        else
        {
            startPauseButton.GetComponent<TMP_Text>().text = "Start";
        }
    }

    /*public void StartPixels() {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        if (texture.GetComponent<Texture>().enabled) texture.GetComponent<Texture>().enabled = false;
        else texture.GetComponent<Texture>().enabled = true;
    }

    public void PausePixels() {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        texture.GetComponent<Texture>().enabled = false;
    }*/
}
