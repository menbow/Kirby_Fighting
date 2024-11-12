using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaddleDee : MonoBehaviour, IEnemyMove
{
    List<EnemyAction> action = new List<EnemyAction>
    {
        new EnemyAction(EnemyMethod.Walk, 2.5f),
        new EnemyAction(EnemyMethod.Frip, 0f),
        new EnemyAction(EnemyMethod.Walk, 2.5f),
    };

    public List<EnemyAction> actionData() => action;

    [SerializeField] float movespeed = 1f;
    public float Movespeed() => movespeed;

    [SerializeField] float jumpPower = 2f;
    public float JumpPower() => jumpPower;

    

}
