using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texture : MonoBehaviour
{
    private int nh;
    private int colorNumber;
    private List<Color32> colorPalette = new List<Color32>();
    private static int textureWidth = 1420;
    private static int textureHeight = 1080;
    private int[,] pixelArray = new int[textureWidth, textureHeight];

    // Start is called before the first frame update
    void Start()
    {
        GameObject optionsCanvas = GameObject.Find("Options Canvas");
        nh = optionsCanvas.GetComponent<Options>().nhoutput;
        colorNumber = optionsCanvas.GetComponent<Options>().colorsoutput;
        SetupColors();
        RandomizeTexture();
        GetComponent<Texture>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Texture2D texture = GetComponent<RawImage>().material.mainTexture as Texture2D;
        int successorIndex;
        bool eval;

        for (int x = 1; x < textureWidth-1; x++)
        {
            for (int y = 1; y < textureHeight-1; y++)
            {
                if (pixelArray[x, y] == colorNumber - 1) successorIndex = 0;
                else successorIndex = pixelArray[x, y] + 1;

                if (nh == 0) eval = vonNeumann(x, y, successorIndex, 1);
                else eval = Moore(x, y, successorIndex, 1);
                
                if (eval)
                //if (Moore(x, y, successorIndex, 1))
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
        //int count = 0;
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
                    if (pixelArray[x, y] == colorNumber - 1) { if (pixelArray[x + i, y + j] == 0) return true; }
                    else { if (pixelArray[x + i, y + j] == (pixelArray[x, y] + 1)) return true; }
                }
            }
        }
        //if (count >= thold) return true;
        //else return false;
        return false;
    }

    private void RandomizeTexture()
    {
        Texture2D texture = new Texture2D(textureWidth, textureHeight);
        GetComponent<RawImage>().material.mainTexture = texture;
        int colorIndex;
        Color color;

        Random.InitState(System.DateTime.Now.Second);

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (x == 0 || y == 0 || x == textureWidth-1 || y == textureHeight-1)
                {
                    colorIndex = -1;
                    color = new Color32(0, 255, 0, 255);
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
        switch (colorNumber) {
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