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
    private bool nh;
    private List<Color32> colorPalette;
    private static int xmargin = 15;
    private static int ymargin = 15;
    private static int border = 5;
    private static int textureWidth = 540 + (2 * xmargin) + (2 * border);
    private static int textureHeight = 540 + (2 * ymargin) + (2 * border);
    private static int xStartIndex = xmargin + border; // starting and ending iteration after and before the borders
    private static int xEndIndex = textureWidth - xmargin - border;
    private static int yStartIndex = ymargin + border;
    private static int yEndIndex = textureHeight - ymargin - border;
    private int[,] pixelArray = new int[textureWidth, textureHeight];   // using 2 arrays, one to read from...
    private int[,] altPixelArray = new int[textureWidth, textureHeight];    // ... and one to write to, for use in th next loop
    private int[,] curr;
    private int[,] next;
    private bool alter = true;
    private Texture2D texture;
    private int successorIndex;

    void Awake()
    {
        updateRule();   // pass the GUI inputs (R/T/C/NH) to the generator
        SetupColors();  // the color palette is set based upon the number of states selected by the user
        RandomizeTexture(); // creates the initial grid of cells randomly
        texture = GetComponent<RawImage>().material.mainTexture as Texture2D;   // get reference to the texture we ll be using
        Array.Copy(pixelArray, altPixelArray, textureWidth * textureHeight);    // copy the values of the array to its clone
        GetComponent<Texture>().enabled = false;    // script is disabled so that it doesnt start updating unless the user presses the Play Button
    }

    private void SetArrays()
    {
        if (alter)
        {
            curr = pixelArray;
            next = altPixelArray;
        }
        else
        {
            curr = altPixelArray;
            next = pixelArray;
        }
        alter = !alter;
    }

    void Update() // MAIN LOOP
    {
        SetArrays();    // alternate using the 2 arrays for reading and writing
        for (int x = xStartIndex; x < xEndIndex; x++)
        {                                                   // for every cell (pixel) in the grid
            for (int y = yStartIndex; y < yEndIndex; y++)
            {
                if (curr[x, y] == colorNumber - 1) successorIndex = 0;
                else successorIndex = curr[x, y] + 1;                   // decide which the successor state is

                if (NeighbourhoodAlgorithm(x, y, successorIndex))   // check whether there are enough neighbours with the successor state around
                {
                    next[x, y] = successorIndex;
                    texture.SetPixel(x, y, colorPalette[curr[x, y]]);   // set the new value if yes
                }
            }
        }
        texture.Apply();
        steps++;
    }

    private bool NeighbourhoodAlgorithm(int x, int y, int successorIndex)
    {
        int count = 0;

        for (int i = -range; i <= range; i++)
        {                                           // check every cell around the current, in a range-sided square
            for (int j = -range; j <= range; j++)
            {
                if (nh || ((Math.Abs(i) + Math.Abs(j)) <= range))   // if Moore is used (nh=true) then go ahead and check every cell in the square grid...
                {                                                   // ... if not then check if the vonNeumann distance is within range
                    if (curr[x + i, y + j] == successorIndex) count++; // increase the total count of successor neighbours
                    if (count >= threshold) return true;    // return true if the count surpasses the threshold
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

        int second = System.DateTime.Now.Second;    // random initial grid is based upon the second and millisecond...
        int millisecond = System.DateTime.Now.Millisecond;  // ... at the time of its call
        int sms = int.Parse(second.ToString() + millisecond.ToString());

        UnityEngine.Random.InitState(sms);

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (x < xmargin || y < ymargin || x >= (textureWidth - xmargin) || y >= (textureHeight - ymargin))
                {
                    colorIndex = -2;
                    color = new Color32(0, 0, 0, 70);       // color of the semitransparent part of the frame
                }
                else if (x < xStartIndex || y < yStartIndex || x >= xEndIndex || y >= yEndIndex)
                {
                    colorIndex = -1;
                    color = new Color32(255, 255, 255, 255);    // white color of the frame
                }
                else
                {
                    colorIndex = UnityEngine.Random.Range(0, colorNumber);  // everything else set randomly
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

    private void SetupColors()  // i wanted each number of states to have a specific color palette...
    {                           // so i hardcoded for every single case
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