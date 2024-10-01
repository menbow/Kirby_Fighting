using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEditor;
using UnityEngine;

public class PlayerMoveByVelosity : MonoBehaviour
{
    private GameObject player;
    private Transform playerTf;
    private Animator animator;
    private AudioSource AS ;

    [SerializeField] AudioClip jumpSE; 
    [SerializeField] AudioClip dashSE;
    [SerializeField] AudioClip graidSE;
    [SerializeField] float dashSpeed = 10;
    [SerializeField] float glaidSpeed = 10;
    [Header("滑空時の落下スピード")]
    [SerializeField] float glaidFallSpeed = 5;
    public bool DashFrag;
    bool graid = false;
    private float h;
    [SerializeField] private bool DashJumpFrag;
    [SerializeField] private float DashTime;
    [SerializeField] float EndDashTime;

    //Range(最小値, 最大値)でInspecter上でいじれる値の範囲を決められる
    [SerializeField, Range(1f, 50f)] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask Ground;
    [SerializeField] LayerMask wall;

    private Rigidbody2D rd;
    private TrailRenderer tl;
    float moveX;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        tl = GetComponent<TrailRenderer>();
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("Dash", DashFrag);
        animator.SetBool("Graid", graid);

        //distance:  「引数名：」を書いてやることでパット見で何の引数書いてるかわかりやすくなる　　また引数名：を書けば引数の順番も変えられる
        RaycastHit2D isGround = Physics2D.CircleCast(transform.position, 1.0f, Vector2.down, distance: 0.4f, Ground);
        //めんどくさかったら　　rd.velocity.y == 0　でとるのもあり
        var GroundTagName = isGround ? isGround.collider.gameObject.tag : "";
        //サークルキャストが地面に当たったら、当たったもののタグをとる


        if (GroundTagName == "Ashiba")
        {
            float v = Input.GetAxisRaw("Vertical");
            var bc = isGround.collider.gameObject.GetComponent<BoxCollider2D>();
            //isGround.collider.gameObject.GetComponent<BoxCollider2D>
            if (v < 0)
            {
                bc.isTrigger = true;
            }
            //下ボタンを押したら足場がすり抜ける
        }

        h = Input.GetAxisRaw("Horizontal");  


        var tempVeloisityY = rd.velocity.y; var tempVelisityX = rd.velocity.x;
        //Jump(tempVelisityX, tempVeloisityY, isGround);

        RaycastHit2D isWall = Physics2D.CircleCast(transform.position, 0.5f, new Vector2(h, 0), 0.3f, wall);

        if (!isWall)
        {
            MoveHorizontal(h, tempVeloisityY, isGround);
        }

        FlipHorizontal(h);


        if (isGround && Input.GetButtonDown("Jump"))
        {
            ResetFallVelosity();
            AS.PlayOneShot(jumpSE);
            rd.velocity = new Vector2(tempVelisityX, jumpForce);
            DashJumpFrag = DashFrag;
            //ジャンプしているときにジャンプダッシュするためのフラグ発動
        }

        graid = false;
        if(rd.velocity.y < 0 && Input.GetButton("Jump"))
        {
            Gliding();
        }


    }

    private void MoveHorizontal(float axisH, float tempVelisityY, bool isGround)
    {

        moveX = axisH * 10;　

        if (DashJumpFrag)　　
        {
            DashJump(axisH, moveX ,isGround);
        }
        else if (DashFrag)　　//普通に地面をダッシュしているとき
        {
            //画像のスケールの向きの方向にダッシュする　　左向きの画像用
            moveX = dashSpeed * (transform.localScale.x * -1);
            DashTime += Time.deltaTime;
        }

        Vector2 move = new Vector2(moveX, tempVelisityY); //最終的な動き

        if (isGround && Input.GetButtonDown("Dash"))
        {
            AS.PlayOneShot(dashSE);
            DashFrag = true;
            move.y = 0.0f;
        }
        rd.velocity = move;

        if (DashTime > EndDashTime)
        {
            DashTime = 0.0f;
            DashFrag = false;
        }

        if (DashFrag)
        {
            tl.emitting = true;
        }
        else
        {
            TrailStop();
        }


    }

    void TrailStop()
    {
        tl.emitting = false;
    }

    void DashJump(float axisH, float moveX ,bool isGround )
    {
            if (Mathf.Abs(axisH) > 0.5f)
            {
                //画像のスケールの向きの方向にダッシュする　　左向きの画像用
                moveX = dashSpeed * (axisH < 0.0f ? -1.0f : 1.0f);
            }
            else
            {
                //画像のスケールの向きの方向にダッシュする　　左向きの画像用
                moveX = dashSpeed * (transform.localScale.x * -1);
            }
            DashTime += Time.deltaTime;

            if(rd.velocity.y < 0 && isGround)　　//ダッシュジャンプ終了の条件
            {
                DashJumpFrag = false;
            }

    }


    private void ResetFallVelosity()
    {
        Vector2 fall = rd.velocity;      //yだけ変えたいときはいったん新しいVecter3型の変数に代入する
        fall.y = 0.0f;                   //yを０にする
        rd.velocity = fall;               //yを変えた変数を代入し直す
    }

    private void Gliding()
    {
        //AS.PlayOneShot(graidSE);
        Vector3 gridMoveHorizontal = new Vector3(glaidSpeed * -transform.localScale.x, -glaidFallSpeed, 0);
        rd.velocity = gridMoveHorizontal;
        //Debug.Log("Gliding");
        graid = true;
    }


    void FlipHorizontal(float axisH)
    {
        if (Mathf.Abs(axisH) > 0.1f)
        {
            Vector3 scale = transform.localScale;    //Xだけ変えたいときはいったん新しいVecter3型の変数に代入する

            scale.x = axisH > 0 ? -1 : 1;   //条件演算子　条件式　？　真の時　：　偽の時
            transform.localScale = scale;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ashiba"))
        {
            //Ashibaタグの足場を通り抜けたら、isTriggerをfalseにし、もう一回乗れるようにする
            var bc = collision.gameObject.GetComponent<BoxCollider2D>();
            bc.isTrigger = false;
        }
    }


}
