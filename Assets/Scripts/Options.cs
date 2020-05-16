using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public GameObject playPauseTMP;
    public GameObject applyButton;
    public GameObject playPauseButton;
    public GameObject resetButton;
    public TMP_Dropdown nhDropdown;
    public GameObject colorsValueText;
    public Slider colorsSlider;
    public int colorsoutput;
    public int nhoutput;
    private bool changesPending = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleColorsSlider()
    {
        colorsoutput = (int)colorsSlider.value;
        colorsValueText.GetComponent<TMP_Text>().text = "" + colorsoutput;
    }

    public void HandleNHInputData(int val)
    {
        nhoutput = val;
    }

    public void ToggleUiElements()
    {
        
        GameObject texture = GameObject.Find("Pixels(Clone)");
        if (texture.GetComponent<Texture>().enabled)
        {
            playPauseTMP.GetComponent<TMP_Text>().text = "Pause";
            colorsSlider.interactable = false;
            nhDropdown.interactable = false;
            applyButton.GetComponent<Button>().interactable = false;
            changesPending = false;
        }
        else
        {
            playPauseTMP.GetComponent<TMP_Text>().text = "Play";
            colorsSlider.interactable = true;
            nhDropdown.interactable = true;
            applyButton.GetComponent<Button>().interactable = true;
            changesPending = true;
        }
        if (changesPending) {
            applyButton.GetComponent<Button>().interactable = true;
            playPauseButton.GetComponent<Button>().interactable = false;
            resetButton.GetComponent<Button>().interactable = false;
        }else{
            applyButton.GetComponent<Button>().interactable = false;
            playPauseButton.GetComponent<Button>().interactable = true;
            resetButton.GetComponent<Button>().interactable = true;
        }
    }
}
