using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyCollider : MonoBehaviour
{

    [Header("�G�̓����蔻��̓g���K�[�łƂ肽���̂�")]
    [Header("����ƃR���C�_�[��G�̎q���ɂ��Ēn�ʂ̓����蔻�������")]
    [Header("�����I�ɑ傫�����킹�Ă����")]


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
