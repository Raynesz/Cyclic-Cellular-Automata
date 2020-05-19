using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private float updateRateSeconds = 4.0F;
    private float dt = 0.0F;
    public float fps = 0.0F;

    void Update()
    {
        dt += Time.unscaledDeltaTime;
        if (dt > 1.0 / updateRateSeconds)
        {
            fps = 1.0f / Time.unscaledDeltaTime;
            dt -= 1.0F / updateRateSeconds;
        }
    }
}
