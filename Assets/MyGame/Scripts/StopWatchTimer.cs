using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatchTimer
{
    private float startTime = 0;

    private bool IsStart = false;

    private float downCountTime = 0;

    public StopWatchTimer() { }

    /// <summary>
    /// 計測開始
    /// </summary>
    public void CountStart()
    {
        startTime = Time.time;
    }

    /// <summary>
    /// 経過時間の取得
    /// </summary>
    /// <returns>経過時間</returns>
    public float GetNowTime()
    {
        return Time.time - startTime;
    }

    /// <summary>
    /// ダウンカウント開始
    /// ただし既に開始してたら無視する
    /// </summary>
    /// <param name="second">ダウンカウンタの時間</param>
    public void DownCountStart(float second)
    {
        if (IsStart)
        {
            return;
        }

        IsStart = true;

        CountStart();

        downCountTime = second;
    }

    /// <summary>
    /// 指定した時間を超えたか
    /// </summary>
    /// <returns>超えたらtrue</returns>
    public bool IsCountEnd()
    {
        return GetNowTime() > downCountTime;
    }

    /// <summary>
    /// 残り時間を取得
    /// </summary>
    /// <returns>残り時間</returns>
    public float GetLeftTime()
    {
        return downCountTime - GetNowTime();
    }

    public float GetLeftPercent()
    {
        return GetLeftTime() / downCountTime;
    }
}
