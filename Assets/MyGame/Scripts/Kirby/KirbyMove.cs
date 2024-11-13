using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public enum KirbyState
{
    Idle, Walk, Jump_Start, Jump_Falling, Jump_FloatFalling,
    Jump_StartFall,
    Floating_Start,
    While_floating,
    Floating_End,
    Jump_FloatEndFalling,
    Jump_FloatJump,
    DoorExit,
    Dash,
    Dash_Stop,
}

[RequireComponent(typeof(StopWatch))]

public class KirbyMove : MonoBehaviour
{
    private GameObject player;
    private Transform playerTf;
    private Animator animator;
    private AudioSource AS ;

    bool DashFrag;
    bool isDash = false;
    bool isFall = false;
    bool isWalking = false;
    private float h;
    private float v;
    public bool moveable;
    public float InputV()
    {
        return v;
    }


    //Range(�ŏ��l, �ő�l)��Inspecter��ł������l�͈̔͂����߂���
    [Tooltip("�����X�s�[�h")]
    [SerializeField, Range(1f, 50f)] float moveSpeed;

    [Tooltip("�󒆂ł̈ڑ����x")]
    [SerializeField, Range(1f, 50f)] float floatMoveSpeed;

    [Tooltip("�_�b�V�����Ă��鎞�̃X�s�[�h")]
    [SerializeField] float dashSpeed = 10;

    [Tooltip("�W�����v��")]
    [SerializeField] float jumpForce;

    [Tooltip("���V���̃W�����v��")]
    [SerializeField] float floatjumpForce;


    [SerializeField, Tooltip("�����Ă��鎞�̃O���r�e�B�X�P�[��")]
    float fallGravity = 3f;

    [SerializeField, Tooltip("�����Ă��Ėc���ł����Ԃ̃O���r�e�B�X�P�[��")]
    float floatGravity = 3f;

    [SerializeField, Tooltip("����f�����Ƃ��ɏo�Ă����C�̃v���n�u")]
    public GameObject breathPrefab;

    [SerializeField, Tooltip("�n�ʂ̃��C���[")]
    LayerMask Ground;

    [SerializeField, Tooltip("�ǂ̃��C���[")]
    LayerMask wall;

    private Rigidbody2D rb;
    private TrailRenderer tl;
    float moveX;
    bool floating = false;
    public bool GetFloating() => floating; 

    KirbyAtacks kirbyAtaks;
    

    public static KirbyState moveState = KirbyState.Idle;
    public KirbyState GetMoveState() => moveState;
    
    //public string Movestate => moveState.ToString();

    /// <summary>///  �n�ʂɗ����Ă���ǂ����̃t���O /// </summary>
    RaycastHit2D isGround;
    public bool GetIsground() => isGround;

    float tempVeloisityY, tempVelisityX;

    //void Anim_Walk() => animator.SetBool("Walk", isWalking);
    //void Anim_Walk() => animator.Play("Walk");

    /// <summary>�@�K�v�ɉ����ăA�j���[�V�������Đ�������֐��@ </summary>
    /// <param name="animName">�A�j���[�V�����R���g���[���[�̃N���b�v��</param>
    /// <param name="enforcementPlay"> true�ŋ����I�ɍŏ�����Đ�������</param>
    void Anim_Action(string animName, bool enforcementPlay = false)
    {
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
        //[0]�͈���Ȃ̂Ŕz��łȂ�
        string clipName = clipInfo.clip.name;


        if (clipName != animName || enforcementPlay)
        {
            //Debug.Log(clipName);
            animator.Play(animName);
        }
        if(clipName == null)
        {
            animator.Play(animName);
        }
    }

    //Action Anim_Walk = () => { };   �����ʂŕ������߂������郉���_��void���Ƃ���  ALT+Enter
    void Anim_Walk() { Anim_Action("Walk"); moveState = KirbyState.Walk; }
    void Anim_Dash() { Anim_Action("Dash"); moveState = KirbyState.Dash; }
    void Anim_Dash_Stop() { Anim_Action("DashStop"); moveState = KirbyState.Dash_Stop; }
    public void Anim_Idle() { Anim_Action("Idle"); moveState = KirbyState.Idle; }
    void Anim_Jump_Start() { Anim_Action("Jump_Start");  moveState = KirbyState.Jump_Start; }
    void Anim_Jump_Falling() { Anim_Action("Jump_Falling"); moveState = KirbyState.Jump_Falling; }
    void Anim_Jump_StartFall() { Anim_Action("Jump_StartFall"); moveState = KirbyState.Jump_StartFall; }
    public void Anim_Jump_FloatFalling() { Anim_Action("Jump_FloatFalling"); moveState = KirbyState.Jump_FloatFalling;}
    void Anim_Jump_FloatEndFalling() { Anim_Action("Jump_FloatEndFalling "); moveState = KirbyState.Jump_FloatEndFalling; }
    void Anim_Floating_Start() { Anim_Action("Floating_Start"); moveState = KirbyState.Floating_Start; }
    public void Anim_DoorExit() { Anim_Action("DoorExit"); moveState = KirbyState.DoorExit; }
    //���V���̎�p�^�p�^�̃A�j���[�V����
    void Anim_FloatJump() { Anim_Action("Jump_FloatJump"); moveState = KirbyState.Jump_FloatJump; }
    void Anim_Floating_End() { Anim_Action("Floating_End"); moveState = KirbyState.Floating_End; }

    //void Anim_Jump() => animator.SetBool("jumping", isGround && Input.GetButtonDown("Jump"));
    void Anim_Float() => animator.SetBool("floating", floating);

    void Animation()
    {
        //animator.SetBool("isGround", isGround);
        //animator.SetBool("floating", floating);
        //animator.SetBool("jumping", isGround && Input.GetButtonDown("Jump"));
        //animator.SetBool("Walk", isWalking);
    }


    StopWatch stopWatch;
    void Start()
    {
        moveable = true;
        AS = GetComponent<AudioSource>();
        tl = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stopWatch = GetComponent<StopWatch>();
        kirbyAtaks = GetComponent<KirbyAtacks>();
    }

    void Update()
    {
        OnGroundSearch();
        //animator.SetBool("Dash", DashFrag);

        h = moveable ? Input.GetAxisRaw("Horizontal") : 0f;
        v = moveable ? Input.GetAxisRaw("Vertical") : 0f;

        //Jump(tempVelisityX);

        //RaycastHit2D isWall = Physics2D.CircleCast(transform.position, 0.5f, new Vector2(h, 0), 0.3f, wall);

        //�ړ��n�̏����͍U�����ɂ͍s��Ȃ�����
        if (kirbyAtaks.GetAtacking() == false�@&& !kirbyAtaks.GetAtacking())
        {
            AtackMethod();
            FlipHorizontal(h);
            Jump();
            tempVeloisityY = rb.velocity.y; tempVelisityX = rb.velocity.x;
            MoveHorizontal(h, tempVeloisityY, isGround);
            ReduceFallSpeed(rb);

        }        //Atack   

    }


    private void MoveHorizontal(float axisH, float tempVelisityY, bool isGround)
    {

        if (isGround)//      �n��
        {
            if (Mathf.Abs(axisH) != 0 && moveState != KirbyState.Walk && moveState != KirbyState.Jump_Start
                && moveState != KirbyState.Floating_End && moveState != KirbyState.Dash)
            {
                //SoundsManager.SE_Play(SE.OnLand);
                Anim_Walk();
                //Debug.Log("����");
            }
            else if (moveState == KirbyState.Jump_FloatFalling || moveState == KirbyState.Jump_FloatJump
                || moveState == KirbyState.Floating_Start)
            {
                Breath();
            }
            else if ((moveState == KirbyState.Walk)
                && Mathf.Abs(axisH) == 0)
            {
                //|| moveState == KirbyState.Floating_End
                Anim_Idle();
            }

        }
        else//    ��
        {
            //�c���Ŗ���������ʏ�̂ɂ���
        }


        //moveX = axisH * 10;

        //axisH���|���邱�Ƃœ��͂��Ȃ������瓮���Ȃ��悤�ɂ���
        moveX = moveSpeed * axisH;

        //Vector2 move = new Vector2(moveX, 0); //�ŏI�I�ȓ���
        Vector2 move = new Vector2(moveX, tempVelisityY); //�ŏI�I�ȓ���

        if (isGround && Input.GetButton("Dash"))
        {

            if (moveState != KirbyState.Jump_Start)
            {
                Anim_Dash();
            }

            if (transform.localScale.x > 0)
            { move.x = dashSpeed; }
            else
            { move.x = -dashSpeed; }

            //move.y = 0.0f;
        }

        if (Input.GetButtonUp("Dash") && isGround)   //�n�ʂɗ����Ă���Ƃ�����
        {
            Anim_Dash_Stop();
        }

        if (Mathf.Abs(rb.velocity.x) <= Mathf.Abs(move.x))
        {
            //Debug.Log(move);
            //rb.AddForce(move * 4,ForceMode2D.Force);
        }

        rb.velocity = move;
    }

    bool dashFrag = false;

    void Dash()
    {
        if(moveState != KirbyState.Dash)
        {

        }
    }
    IEnumerator DashFragCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
    }


    public void Breath()
    {
        if (moveState == KirbyState.Jump_FloatJump || moveState == KirbyState.Jump_FloatFalling
        || moveState == KirbyState.Floating_Start)
        {
            GameObject breath = Instantiate(breathPrefab,transform.position, Quaternion.identity);
            BreathMove aaa = breath.GetComponent<BreathMove>();

            //Debug.Log(aaa);
            StartCoroutine(aaa.BreathCoroutine(transform.localScale.x));


            //Debug.Log("���f��");
            Anim_Floating_End();
            SoundsManager.SE_Play(SE.flyspit);
            //����f����������������
        }

    }

    public void BreathEnd()
    {
        if (!isGround)
        {
            Anim_Jump_StartFall();
        }
        if (isGround)
        {
            Anim_Idle();
        }
    }

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

        if (isGround)
        {
            floating = false;
        }
    }

    private void ResetFallVelosity()
    {
        Vector2 fall = rb.velocity;      //y�����ς������Ƃ��͂�������V����Vecter3�^�̕ϐ��ɑ������
        fall.y = 0.0f;                   //y���O�ɂ���
        rb.velocity = fall;               //y��ς����ϐ�����������
    }

    /// <summary>
    /// �������x���Ǘ�����֐��ł�
    /// </summary>
    /// <param name="rb">�J�[�r�B�[���g��RigidBody</param>
    private void ReduceFallSpeed(Rigidbody2D rb)
    {
        if(floating)
        {
            //if(moveState != KirbyState.Floating_Start || moveState != KirbyState.Jump_FloatJump)
            //{
            //    Anim_While_floating();
            //}
            rb.gravityScale = floatGravity;
        }
        else
        {
            rb.gravityScale = fallGravity;
        }

    }


    void AtackMethod()
    {
        if(!isGround && Input.GetButtonDown("Atack"))
        {
            floating = false;
            Breath();
        }
    }

    void Jump()
    {
        if (isGround && Input.GetButtonDown("Jump"))
        {
            //Debug.Log("jump");
            ResetFallVelosity();
            //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            rb.velocity = new Vector2(tempVelisityX, jumpForce);
            //�W�����v���Ă���Ƃ��ɃW�����v�_�b�V�����邽�߂̃t���O����
            Anim_Jump_Start();
            SoundsManager.SE_Play(SE.Jump);
            return;
        }
        if (!isGround && Input.GetButtonDown("Jump"))
        {
            ResetFallVelosity();
            rb.velocity = new Vector2(tempVelisityX, floatjumpForce);
            SoundsManager.SE_Play(SE.fly);
            floating = true;
            if(moveState == KirbyState.While_floating || moveState == KirbyState.Floating_Start
                 || moveState == KirbyState.Jump_FloatFalling || moveState == KirbyState.Jump_FloatJump)
            {
                //�����A�j���[�V�����Đ������Ă����[�v���Ȃ��̂ŗ������̃A�j���[�V���������܂Ȃ��Ƃ����Ȃ�
                Anim_FloatJump();
                return;
            }
            else { Anim_Floating_Start(); }
        }

        if(!isGround && Mathf.Abs(rb.velocity.y) <= 0.5 
            && moveState != KirbyState.While_floating && moveState != KirbyState.Floating_End
            && !floating)
        {
            Anim_Jump_StartFall();
            return;
        }


        if ( isGround && moveState == KirbyState.Jump_StartFall)
        { Anim_Idle();  return; }
    }


    void FlipHorizontal(float axisH)
    {
        if (Mathf.Abs(axisH) > 0.1f)
        {
            Vector3 scale = transform.localScale;    //X�����ς������Ƃ��͂�������V����Vecter3�^�̕ϐ��ɑ������

            scale.x = axisH > 0 ? 1 : -1;   //�������Z�q�@�������@�H�@�^�̎��@�F�@�U�̎�
            transform.localScale = scale;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ashiba"))
        {
            //Ashiba�^�O�̑����ʂ蔲������AisTrigger��false�ɂ��A����������悤�ɂ���
            var bc = collision.gameObject.GetComponent<BoxCollider2D>();
            bc.isTrigger = false;
        }
    }



}
