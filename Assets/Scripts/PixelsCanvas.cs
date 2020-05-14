using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelsCanvas : MonoBehaviour
{
    public GameObject pixelsCanvas;
    public GameObject pixelsPrefab;
    private GameObject pixels;

    // Start is called before the first frame update
    void Start()
    {
        CreatePixels();
    }

    public void CreatePixels() {
        pixels = Instantiate(pixelsPrefab);
        pixels.transform.SetParent(pixelsCanvas.transform);
        pixels.GetComponent<RectTransform>().offsetMin = new Vector2(500, 0);
        pixels.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
    }

    public void DestroyPixels() {
        Destroy(pixels);
    }

    public void ResetPixels() {
        DestroyPixels();
        CreatePixels();
    }

    public void StartPixels() {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        if (texture.GetComponent<Texture>().enabled) texture.GetComponent<Texture>().enabled = false;
        else texture.GetComponent<Texture>().enabled = true;
    }

    public void PausePixels() {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        texture.GetComponent<Texture>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
