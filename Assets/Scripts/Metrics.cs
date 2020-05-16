using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Metrics : MonoBehaviour
{
    public GameObject rulesText;
    public GameObject stepsText;
    public GameObject fpsText;
    private GameObject optionsCanvas;
    private GameObject texture;
    private int colorNumber;
    private int steps;
    private string nh;
    private int fps;
    // Start is called before the first frame update
    void Start()
    {
        optionsCanvas = GameObject.Find("Options Canvas");
        rulesText.GetComponent<TMP_Text>().text = "Rule: ";
        stepsText.GetComponent<TMP_Text>().text = "Steps: 0";
        fpsText.GetComponent<TMP_Text>().text = "FPS: ";
    }

    public void onPlay() {
        if (optionsCanvas.GetComponent<Options>().nhoutput == 0) nh = "vonNeumann";
        else nh = "Moore";
        colorNumber = optionsCanvas.GetComponent<Options>().colorsoutput;
        rulesText.GetComponent<TMP_Text>().text = "Rule: C"+colorNumber+"/"+nh;
    }

    // Update is called once per frame
    void Update()
    {
        texture = GameObject.Find("Pixels(Clone)");
        steps = texture.GetComponent<Texture>().steps;
        fps = (int)fpsText.GetComponent<FPS>().fps;
        stepsText.GetComponent<TMP_Text>().text = "Steps: "+steps;
        fpsText.GetComponent<TMP_Text>().text = "FPS: "+fps;
    }
}
