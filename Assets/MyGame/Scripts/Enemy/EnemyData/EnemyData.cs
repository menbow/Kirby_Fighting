using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyScript : MonoBehaviour
{
    List<EnemyAction> actionData = EnemyDataList.waddleDee;

    List<EnemyAction> move_WaddleDee = new()
    { 
        //EnemyAction
        new (EnemyMethod.Walk, 2.5f),
        new(EnemyMethod.Frip, 0f),
        new(EnemyMethod.Walk, 2.5f), 
    };
}
