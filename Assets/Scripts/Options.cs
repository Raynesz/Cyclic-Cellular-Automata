using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public GameObject playPauseTMP;
    public int colorsoutput;
    public int nhoutput=0;
    public GameObject myText;
    public Slider colorsSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleColorsSlider() {
        colorsoutput = (int)colorsSlider.value;
        myText.GetComponent<TMP_Text>().text = ""+colorsoutput;
    }

    public void HandleNHInputData(int val) {
        nhoutput = val;
    }

    public void SwapButtonText()
    {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        if (texture.GetComponent<Texture>().enabled) playPauseTMP.GetComponent<TMP_Text>().text = "Pause";
        else playPauseTMP.GetComponent<TMP_Text>().text = "Play";
    }
}
