using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMethod
{
    Walk,Jump,Atack,Away,AirAtack,Frip,Run,Chase
}

[RequireComponent(typeof(StopWatch))]
public partial class EnemyScript : MonoBehaviour
{
    BoxCollider2D col;
    public BoxCollider2D GetCol() => col;

    Rigidbody2D rb; StopWatch sw;

    IEnemyMove enemyData;

    [SerializeField, Tooltip("�n�ʂ̃��C���[")]
    LayerMask Ground;

    RaycastHit2D isGround;

    /// <summary> ���ǂݍ���ł�actionData�̔ԍ� </summary>
    int currentDataNum = 0;
    /// <summary> ���ǂݍ���ł�actionData�̏������I���Ă邩�ǂ��� </summary>
    bool moveDone = true;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sw = GetComponent<StopWatch>();
        rb = GetComponent<Rigidbody2D>();
        enemyData = GetComponent<IEnemyMove>();

    }


    void Update()
    {
        OnGroundSearch();

        //������Ă鏈�����I��点�Ă�����
        if (moveDone)
        {
            ReadMoveData();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    void ReadMoveData()
    {
        moveDone = false;
            switch (enemyData.actionData()[currentDataNum].method)
            {
                case EnemyMethod.Walk:
                    StartCoroutine(Walk(enemyData.actionData()[currentDataNum],enemyData.Movespeed()));
                    break;

                case EnemyMethod.Jump:
                    Jump(enemyData.JumpPower());
                    break;

                case EnemyMethod.Atack:
                    break;

                    case EnemyMethod.Away:
                    break;

                    case EnemyMethod.Frip:
                    Frip();
                    break;

            }

        currentDataNum++;
        currentDataNum = currentDataNum % enemyData.actionData().Count;

    }


    void NextReadData()
    {

    }


    //-----------------------�����̃��\�b�h------------------------------------------------
    //

    IEnumerator Walk(EnemyAction actionData, float speed)
    {
        Vector2 dir = new Vector2(speed * transform.localScale.x, rb.velocity.y);
        rb.velocity = dir;
        //Debug.Log(rb.velocity);
        yield return new WaitForSeconds(actionData.time);
        moveDone = true;
    }

    void Jump(float jumpForce)
    {
        if (isGround)
        {
            Vector2 Force = new Vector2(0, jumpForce);
            rb.velocity = Force;
        }
        moveDone = true;
    }

    void Atack()
    {
        moveDone = true;
    }

    /// <summary>
    /// ���g�̃X�P�[��
    /// </summary>
    void Frip()
    {
        //Debug.Log("Frip");
        Vector3 value = transform.localScale;
        value.x *= -1;
        transform.localScale = value;
        moveDone = true;
    }

    //IEnumerator ChaceCoroutine()
    //{

    //}


    /// <summary>/// �n�ʂɗ����Ă��邩�ǂ����𒲂ׂ� /// </summary>
    void OnGroundSearch()
    {
        //�T�[�N���L���X�g�͔��a�����������ĕǂ̔�����Ƃ��Ă��܂��̂�h���ł���

        //distance:  �u�������F�v�������Ă�邱�ƂŃp�b�g���ŉ��̈��������Ă邩�킩��₷���Ȃ�@�@�܂��������F�������Έ����̏��Ԃ��ς�����
        isGround = Physics2D.CircleCast(transform.position, 0.2f, Vector2.down, distance: 1f, Ground);
        //�߂�ǂ�����������@�@rd.velocity.y == 0�@�łƂ�̂�����
        var GroundTagName = isGround ? isGround.collider.gameObject.tag : "";
        //�T�[�N���L���X�g���n�ʂɓ���������A�����������̂̃^�O���Ƃ�


        if (GroundTagName == "Ashiba")
        {
            float v = Input.GetAxisRaw("Vertical");
            var bc = isGround.collider.gameObject.GetComponent<BoxCollider2D>();
            //isGround.collider.gameObject.GetComponent<BoxCollider2D>
            if (v < 0)
            {
                bc.isTrigger = true;
            }
            //���{�^�����������瑫�ꂪ���蔲����
        }

    }

}
