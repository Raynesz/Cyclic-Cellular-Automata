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
    private bool changesPending = false;
    // Start is called before the first frame update
    void Start()
    {
        applyButton.GetComponent<Button>().interactable = false;
        playPauseButton.GetComponent<Button>().interactable = true;
        resetButton.GetComponent<Button>().interactable = true;
    }

    public void HandleColorsSlider()
    {
        changesPending = true;
        colorsoutput = (int)colorsSlider.value;
        colorsValueText.GetComponent<TMP_Text>().text = "" + colorsoutput;
    }

    public void HandleNHInputData(int val)
    {
        changesPending = true;
        nhoutput = val;
    }

    public void onApply() {
        changesPending = false;
    }

    public void ToggleUiElements()
    {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        if (texture.GetComponent<Texture>().enabled)
        {
            playPauseTMP.GetComponent<TMP_Text>().text = "Pause";
            colorsSlider.interactable = false;
            nhDropdown.interactable = false;
        }
        else
        {
            playPauseTMP.GetComponent<TMP_Text>().text = "Play";
            colorsSlider.interactable = true;
            nhDropdown.interactable = true;
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