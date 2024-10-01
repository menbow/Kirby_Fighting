using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StopWatchTest : MonoBehaviour
{
    StopWatch StopWatch;
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StopWatch = GetComponent<StopWatch>();
        StopWatch.CountStart();
    }

    void Update()
    {
        text.text = StopWatch.GetTime().ToString();
    }
}
