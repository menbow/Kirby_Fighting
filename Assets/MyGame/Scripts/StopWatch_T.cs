using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch_T
{
    float startTime = 0;

    public void CountStart()
    {
        startTime = Time.time;
    }

    public float GetTime()
    {
        return Time.time - startTime;
    }
}
