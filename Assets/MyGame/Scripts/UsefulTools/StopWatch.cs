using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch : MonoBehaviour
{

    bool isStart = false;
    float startTime = 0;

    public void CountStart()
    {
        startTime = Time.time;
        isStart = true;
    }

    public float GetTime()
    {
        return Time.time - startTime;
    }

    /// <summary>
    /// 指定した秒数の間隔でtrueを返す
    /// </summary>
    /// <param name="interval">間隔の秒数</param>
    /// <returns>インターバル時間を越えているかどうか</returns>
    public bool HitTiming(float interval)
    {
        if(isStart == false)
        {
            CountStart();
            //起動した時の最初のヒット
            return true;
        }

        if (GetTime() > interval)
        {
            CountStart();
            return true;
        }
            
        //この時点でインターバル越えてないのが確定する
        return false;
    }
}
