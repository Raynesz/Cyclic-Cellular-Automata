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
    public GameObject rangeValueText;
    public Slider rangeSlider;
    public GameObject thresholdValueText;
    public Slider thresholdSlider;
    public GameObject colorsValueText;
    public Slider colorsSlider;
    public int rangeoutput;
    public int thresholdoutput;
    public int colorsoutput;
    public bool nhoutput;
    private bool changesPending = false;

    void Start()
    {
        applyButton.GetComponent<Button>().interactable = false;
        playPauseButton.GetComponent<Button>().interactable = true;
        resetButton.GetComponent<Button>().interactable = true;
    }

    public void HandleRangeSlider()
    {
        changesPending = true;
        rangeoutput = (int)rangeSlider.value;
        rangeValueText.GetComponent<TMP_Text>().text = "" + rangeoutput;
    }

    public void HandleThresholdSlider()
    {
        changesPending = true;
        thresholdoutput = (int)thresholdSlider.value;
        thresholdValueText.GetComponent<TMP_Text>().text = "" + thresholdoutput;
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
        if (val == 0) nhoutput = false;
        else nhoutput = true;
    }

    public void onApply()
    {
        changesPending = false;
    }

    public void ToggleUiElements()
    {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        if (texture.GetComponent<Texture>().enabled)
        {
            playPauseTMP.GetComponent<TMP_Text>().text = "Pause";
            rangeSlider.interactable = false;
            thresholdSlider.interactable = false;
            colorsSlider.interactable = false;
            nhDropdown.interactable = false;
        }
        else
        {
            playPauseTMP.GetComponent<TMP_Text>().text = "Play";
            rangeSlider.interactable = true;
            thresholdSlider.interactable = true;
            colorsSlider.interactable = true;
            nhDropdown.interactable = true;
        }

        if (changesPending)
        {
            applyButton.GetComponent<Button>().interactable = true;
            playPauseButton.GetComponent<Button>().interactable = false;
            resetButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            applyButton.GetComponent<Button>().interactable = false;
            playPauseButton.GetComponent<Button>().interactable = true;
            resetButton.GetComponent<Button>().interactable = true;
        }
    }
}