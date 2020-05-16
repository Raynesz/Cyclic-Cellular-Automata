using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private float updateRateSeconds = 4.0F;
    private int frameCount = 0;
    private float dt = 0.0F;
    public float fps = 0.0F;

    void Update()
    {
        frameCount++;
        dt += Time.unscaledDeltaTime;
        if (dt > 1.0 / updateRateSeconds)
        {
            fps = 1.0f / Time.unscaledDeltaTime;
            frameCount = 0;
            dt -= 1.0F / updateRateSeconds;
        }
    }
}
