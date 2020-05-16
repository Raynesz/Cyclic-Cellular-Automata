using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private int frameCount = 0;
    private double dt = 0.0;
    public double fps = 0.0;
    private double updateRate = 4.0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0/updateRate)
        {
            fps = frameCount / dt ;
            frameCount = 0;
            dt -= 1.0/updateRate;
        }
    }
}
