﻿using UnityEngine;
using TMPro;

public class Metrics : MonoBehaviour
{
    public GameObject rulesText;
    public GameObject stepsText;
    public GameObject fpsText;
    private GameObject optionsCanvas;
    private GameObject texture;
    private int range;
    private int threshold;
    private int colorNumber;
    private int steps = 0;
    private string nh;
    private float fps;

    void Start()
    {
        optionsCanvas = GameObject.Find("Options Canvas");
        rulesText.GetComponent<TMP_Text>().text = "Rule: ";
        stepsText.GetComponent<TMP_Text>().text = "Steps: ";
        fpsText.GetComponent<TMP_Text>().text = "FPS: ";
    }

    public void onPlay()
    {
        range = optionsCanvas.GetComponent<Options>().rangeoutput;
        threshold = optionsCanvas.GetComponent<Options>().thresholdoutput;
        colorNumber = optionsCanvas.GetComponent<Options>().colorsoutput;
        if (optionsCanvas.GetComponent<Options>().nhoutput) nh = "Moore";
        else nh = "vonNeumann";
        rulesText.GetComponent<TMP_Text>().text = "Rule: R" + range + "/T" + threshold + "/C" + colorNumber + "/" + nh;
    }

    void FixedUpdate()  // metrics display will only be updated a few times every second
    {
        texture = GameObject.Find("Pixels(Clone)");
        steps = texture.GetComponent<Texture>().steps;
        fps = fpsText.GetComponent<FPS>().fps;
        fps = Mathf.Round(fps * 10.0f) * 0.1f;
        stepsText.GetComponent<TMP_Text>().text = "Steps: " + steps;
        fpsText.GetComponent<TMP_Text>().text = "FPS: " + fps;
    }
}