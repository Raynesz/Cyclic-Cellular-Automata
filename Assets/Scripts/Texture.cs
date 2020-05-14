using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texture : MonoBehaviour
{
    private int colorNumber;
    private List<Color32> colorPalette = new List<Color32>();
    private int[,] pixelArray = new int[1420, 1080];

    // Start is called before the first frame update
    void Start()
    {
        SetupColors();
        RandomizeTexture();
        GetComponent<Texture>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Texture2D texture = GetComponent<RawImage>().material.mainTexture as Texture2D;
        int successorIndex;

        for (int x = 1; x < 1419; x++)
        {
            for (int y = 1; y < 1079; y++)
            {
                if (pixelArray[x, y] == colorNumber - 1) successorIndex = 0;
                else successorIndex = pixelArray[x, y] + 1;

                if (vonNeumann(x, y, successorIndex, 1))
                //if (Moore(1, x, y))
                //if (true)
                {
                    pixelArray[x, y] = successorIndex;
                    texture.SetPixel(x, y, colorPalette[pixelArray[x, y]]);
                }
            }
        }
        texture.Apply();
    }

    private bool vonNeumann(int x, int y, int successorIndex, int thold)
    {
        //int count = 0;

        if (pixelArray[x, y + 1] == successorIndex || pixelArray[x + 1, y] == successorIndex || pixelArray[x, y - 1] == successorIndex || pixelArray[x - 1, y] == successorIndex)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool Moore(int x, int y, int successorIndex, int thold)
    {
        int count = 0;
        int left = -1, bottom = -1;
        int right = 1, top = 1;

        if (x == 0)
        {
            left = 0;
        }

        if (x == 1419)
        {
            right = 0;
        }

        if (y == 0)
        {
            bottom = 0;
        }

        if (y == 1079)
        {
            top = 0;
        }

        for (int i = left; i <= right; i++)
        {
            for (int j = bottom; j <= top; j++)
            {
                if (i != j)
                {
                    if (pixelArray[x, y] == colorNumber - 1) { if (pixelArray[x + i, y + j] == 0) count++; }
                    else { if (pixelArray[x + i, y + j] == (pixelArray[x, y] + 1)) count++; }
                }
            }
        }
        if (count >= thold) return true;
        else return false;
    }

    private void RandomizeTexture()
    {
        Texture2D texture = new Texture2D(1420, 1080);
        GetComponent<RawImage>().material.mainTexture = texture;
        int colorIndex;
        Color color;

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (x == 0 || y == 0 || x == 1419 || y == 1079)
                {
                    colorIndex = -1;
                    color = new Color32(0, 0, 0, 255);
                }
                else
                {
                    colorIndex = Random.Range(0, colorNumber);
                    color = colorPalette[colorIndex];
                }
                pixelArray[x, y] = colorIndex;
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }

    private void SetupColors()
    {
        /*colorPalette.Add(new Color32(255, 0, 0, 255));
        colorPalette.Add(new Color32(255, 153, 0, 255));
        colorPalette.Add(new Color32(255, 255, 0, 255));
        colorPalette.Add(new Color32(0, 255, 0, 255));
        colorPalette.Add(new Color32(0, 0, 255, 255));
        colorPalette.Add(new Color32(63, 0, 255, 255));
        colorPalette.Add(new Color32(127, 0, 255, 255));*/
        GameObject optionsCanvas = GameObject.Find("Options Canvas");
        colorNumber = optionsCanvas.GetComponent<Options>().output;
        //Debug.Log(colorNumber);
        if (colorNumber == 18) colorPalette = new List<Color32>{
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
        else if (colorNumber == 12) colorPalette = new List<Color32>{
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
        else colorPalette = new List<Color32>{
            new Color32(255, 0, 0, 255), //red
            new Color32(255, 153, 0, 255), //orange
            new Color32(255, 255, 0, 255), //yellow
            new Color32(0, 255, 0, 255), //green
            new Color32(0, 0, 255, 255), //blue
            new Color32(127, 0, 255, 255) //violet
            };
    }
}