using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public GameObject startPauseTMP;
    public int output=18;

    public GameObject myText;
    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleSlider() {
        myText.GetComponent<TMP_Text>().text = ""+mySlider.value;
    }

    public void HandleInputData(int val) {
        if (val == 0) output = 18;
        else if (val == 1) output = 12;
        else output = 6;
    }

    public void SwapButtonText()
    {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        if (texture.GetComponent<Texture>().enabled) startPauseTMP.GetComponent<TMP_Text>().text = "Pause";
        else startPauseTMP.GetComponent<TMP_Text>().text = "Start";
    }
}
