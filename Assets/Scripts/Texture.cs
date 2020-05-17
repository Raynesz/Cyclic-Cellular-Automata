using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Texture : MonoBehaviour
{
    public int steps = 0;
    private int range;
    private int threshold;
    private int colorNumber;
    private int nh;
    private List<Color32> colorPalette = new List<Color32>();
    private static int textureWidth = 800;
    private static int textureHeight = 600;
    private int[,] pixelArray = new int[textureWidth, textureHeight];
    private int[,] dummy = new int[textureWidth, textureHeight];
    private int[,] curr;
    private int[,] next;
    private bool alter = true;
    private Texture2D texture;
    private int successorIndex;
    private bool eval;

    // Start is called before the first frame update
    void Start()
    {
        updateRule();
        SetupColors();
        RandomizeTexture();
        texture = GetComponent<RawImage>().material.mainTexture as Texture2D;
        GetComponent<Texture>().enabled = false;
        Array.Copy(pixelArray, dummy, textureWidth * textureHeight);
    }

    private void SetArrays()
    {
        if (alter)
        {
            curr = pixelArray;
            next = dummy;
        }
        else
        {
            curr = dummy;
            next = pixelArray;
        }
        alter = !alter;
    }

    // Update is called once per frame
    void Update()
    {
        SetArrays();
        for (int x = 2; x < textureWidth - 2; x++)
        {
            for (int y = 2; y < textureHeight - 2; y++)
            {
                if (curr[x, y] == colorNumber - 1) successorIndex = 0;
                else successorIndex = curr[x, y] + 1;

                if (nh == 0) eval = vonNeumann(x, y, successorIndex);
                else eval = Moore(x, y, successorIndex);

                if (eval)
                {
                    next[x, y] = successorIndex;
                    texture.SetPixel(x, y, colorPalette[curr[x, y]]);
                }
            }
        }
        texture.Apply();
        steps++;
    }

    private bool vonNeumann(int x, int y, int successorIndex)
    {
        int count = 0;

        if (curr[x, y + 1] == successorIndex) count++;
        if (curr[x + 1, y] == successorIndex) count++;
        if (curr[x, y - 1] == successorIndex) count++;
        if (curr[x - 1, y] == successorIndex) count++;
        if (count >= threshold) return true;
        else return false;
    }

    private bool Moore(int x, int y, int successorIndex)
    {
        int count = 0;

        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                if (((x + i) >= 0 && (x + i) <= textureWidth - 1) && ((y + j) >= 0 && (y + j) <= textureHeight - 1))
                {
                    if (curr[x + i, y + j] == successorIndex) count++;
                }
                if (count >= threshold)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void RandomizeTexture()
    {
        Texture2D texture = new Texture2D(textureWidth, textureHeight);
        GetComponent<RawImage>().material.mainTexture = texture;
        int colorIndex;
        Color color;

        UnityEngine.Random.InitState(System.DateTime.Now.Second);

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (x < 2 || y < 2 || x > textureWidth - 3 || y > textureHeight - 3)
                {
                    colorIndex = -1;
                    color = new Color32(255, 255, 255, 255);
                }
                else
                {
                    colorIndex = UnityEngine.Random.Range(0, colorNumber);
                    color = colorPalette[colorIndex];
                }
                pixelArray[x, y] = colorIndex;
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }

    private void updateRule()
    {
        GameObject optionsCanvas = GameObject.Find("Options Canvas");
        range = optionsCanvas.GetComponent<Options>().rangeoutput;
        threshold = optionsCanvas.GetComponent<Options>().thresholdoutput;
        colorNumber = optionsCanvas.GetComponent<Options>().colorsoutput;
        nh = optionsCanvas.GetComponent<Options>().nhoutput;
    }

    private void SetupColors()
    {
        switch (colorNumber)
        {
            case 18:
                colorPalette = new List<Color32>{
                new Color32(181, 0, 0, 255), //dark red
                new Color32(255, 0, 0, 255), //red
                new Color32(211, 46, 0, 255), //red - orange
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(185, 255, 0, 255), //yellow green
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 157, 0, 255), //dark green
                new Color32(0, 254, 0, 107), //dark blue green
                new Color32(0, 157, 99, 255), //bright green blue
                new Color32(0, 255, 255, 255), //bright blue
                new Color32(0, 0, 255, 255), //blue
                new Color32(0, 0, 136, 255), //dark blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                new Color32(216, 0, 255, 255), //mauve
                new Color32(255, 0, 181, 255) //pink
                };
                break;
            case 17:
                colorPalette = new List<Color32>{
                new Color32(181, 0, 0, 255), //dark red
                new Color32(255, 0, 0, 255), //red
                new Color32(211, 46, 0, 255), //red - orange
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(185, 255, 0, 255), //yellow green
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 254, 0, 107), //dark blue green
                new Color32(0, 157, 99, 255), //bright green blue
                new Color32(0, 255, 255, 255), //bright blue
                new Color32(0, 0, 255, 255), //blue
                new Color32(0, 0, 136, 255), //dark blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                new Color32(216, 0, 255, 255), //mauve
                new Color32(255, 0, 181, 255) //pink
                };
                break;
            case 16:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(211, 46, 0, 255), //red - orange
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(185, 255, 0, 255), //yellow green
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 254, 0, 107), //dark blue green
                new Color32(0, 157, 99, 255), //bright green blue
                new Color32(0, 255, 255, 255), //bright blue
                new Color32(0, 0, 255, 255), //blue
                new Color32(0, 0, 136, 255), //dark blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                new Color32(216, 0, 255, 255), //mauve
                new Color32(255, 0, 181, 255) //pink
                };
                break;
            case 15:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(211, 46, 0, 255), //red - orange
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(185, 255, 0, 255), //yellow green
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 254, 0, 107), //dark blue green
                new Color32(0, 157, 99, 255), //bright green blue
                new Color32(0, 255, 255, 255), //bright blue
                new Color32(0, 0, 255, 255), //blue
                new Color32(0, 0, 136, 255), //dark blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                new Color32(255, 0, 181, 255) //pink
                };
                break;
            case 14:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(211, 46, 0, 255), //red - orange
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(185, 255, 0, 255), //yellow green
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 157, 99, 255), //bright green blue
                new Color32(0, 255, 255, 255), //bright blue
                new Color32(0, 0, 255, 255), //blue
                new Color32(0, 0, 136, 255), //dark blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                new Color32(255, 0, 181, 255) //pink
                };
                break;
            case 13:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(211, 46, 0, 255), //red - orange
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(185, 255, 0, 255), //yellow green
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 255, 255, 255), //bright blue
                new Color32(0, 0, 255, 255), //blue
                new Color32(0, 0, 136, 255), //dark blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                new Color32(255, 0, 181, 255) //pink
                };
                break;
            case 12:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(211, 46, 0, 255), //red - orange
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(185, 255, 0, 255), //yellow green
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 255, 255, 255), //bright blue
                new Color32(0, 0, 255, 255), //blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                new Color32(255, 0, 181, 255) //pink
                };
                break;
            case 11:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(211, 46, 0, 255), //red - orange
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(185, 255, 0, 255), //yellow green
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 0, 255, 255), //blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                new Color32(255, 0, 181, 255) //pink
                };
                break;
            case 10:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(211, 46, 0, 255), //red - orange
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 0, 255, 255), //blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                new Color32(255, 0, 181, 255) //pink
                };
                break;
            case 9:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 0, 255, 255), //blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                new Color32(255, 0, 181, 255) //pink
                };
                break;
            case 8:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(255, 153, 0, 255), //orange
                new Color32(250, 180, 0, 255), //orange - yellow
                new Color32(255, 255, 0, 255), //yellow
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 0, 255, 255), //blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                };
                break;
            case 7:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(255, 153, 0, 255), //orange
                new Color32(255, 255, 0, 255), //yellow
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 0, 255, 255), //blue
                new Color32(63, 0, 255, 255), //indigo
                new Color32(127, 0, 255, 255), //violet
                };
                break;
            case 6:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(255, 153, 0, 255), //orange
                new Color32(255, 255, 0, 255), //yellow
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 0, 255, 255), //blue
                new Color32(127, 0, 255, 255) //violet
                };
                break;
            case 5:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(255, 153, 0, 255), //orange
                new Color32(0, 255, 0, 255), //green
                new Color32(0, 0, 255, 255), //blue
                new Color32(127, 0, 255, 255) //violet
                };
                break;
            case 4:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(255, 255, 0, 255), //yellow
                new Color32(0, 255, 0, 255), //green
                new Color32(127, 0, 255, 255) //violet
                };
                break;
            case 3:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(0, 255, 0, 255), //green
                new Color32(127, 0, 255, 255) //violet
                };
                break;
            default:
                colorPalette = new List<Color32>{
                new Color32(255, 0, 0, 255), //red
                new Color32(0, 255, 0, 255), //green
                };
                break;
        }
    }
}