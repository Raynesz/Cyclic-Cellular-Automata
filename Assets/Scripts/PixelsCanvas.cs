﻿using UnityEngine;

public class PixelsCanvas : MonoBehaviour
{
    public GameObject pixelsCanvas;
    public GameObject pixelsPrefab;
    private GameObject pixels;

    void Start()
    {
        CreatePixels();
    }

    public void CreatePixels()
    {
        pixels = Instantiate(pixelsPrefab);
        pixels.transform.SetParent(pixelsCanvas.transform, false);
    }

    public void DestroyPixels()
    {
        Destroy(pixels);
    }

    public void ResetPixels()
    {
        DestroyPixels();
        CreatePixels();
    }

    public void StartPixels()
    {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        if (texture.GetComponent<Texture>().enabled) texture.GetComponent<Texture>().enabled = false;
        else texture.GetComponent<Texture>().enabled = true;
    }

    public void PausePixels()
    {
        GameObject texture = GameObject.Find("Pixels(Clone)");
        texture.GetComponent<Texture>().enabled = false;
    }
}