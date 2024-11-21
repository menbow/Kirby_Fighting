using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyCollider : MonoBehaviour
{

    [Header("敵の当たり判定はトリガーでとりたいので")]
    [Header("これとコライダーを敵の子供につけて地面の当たり判定をつける")]
    [Header("自動的に大きさ合わせてくれる")]


    EnemyScript enemyScript;
    BoxCollider2D myCollider;
    BoxCollider2D parentCollider;


    void Awake()
    {
        SetCol();
    }

    void SetCol()
    {
        enemyScript = GetComponentInParent<EnemyScript>();
        parentCollider = enemyScript.GetCol();
        myCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if(parentCollider == null)
        {
            SetCol() ;
        }
        if (parentCollider != null)
        {
            Debug.Log(parentCollider);
            myCollider.size = parentCollider.size;
        }
    }

}
