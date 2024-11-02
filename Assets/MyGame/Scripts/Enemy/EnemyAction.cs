using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAction
{
    public EnemyMethod method;
    public float time;


    /// <summary>
    /// 敵が行う行動と行う時間を指定する
    /// </summary>
    /// <param name="method"></param>
    /// <param name="time"></param>
    public EnemyAction(EnemyMethod method, float time = 2f)
    {
        this.method = method;
        this.time = time;
    }
}
