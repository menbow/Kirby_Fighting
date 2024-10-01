using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform tf;
    private Transform playerTf;
    [SerializeField] float cameraBottom = 1f;
    [SerializeField] float cameraSpeed = 2f;
    [SerializeField] private bool moveY = false;  //縦スクロールにしたいとき
    [SerializeField] private bool moveX = true;
    [Header("Rigidbody系の移動ならFixedで")]
    public UpdateType updatetype = UpdateType.Fixed;

    void Start()
    {
        tf = GetComponent<Transform>();
        var player = GameObject.FindGameObjectWithTag("Player");
        playerTf = player.GetComponent<Transform>();
    }

    //可変フレームレート
    void Update()
    {
        //カメラのX座標をプレイヤーのX座標と同じにする方法  
        //これだとカメラが完璧についてくるので酔いやすい
        //tf.position = new Vector3(playerTf.position.x, tf.position.y, tf.position.z);


        //端っこに来たら止めるやつ
        if (tf.position.x < 0)
        {
            tf.position = new Vector3(0, transform.position.y, tf.position.z);
        }

        if (tf.position.y <= cameraBottom)
        {
            tf.position = new Vector3(tf.position.x, cameraBottom, tf.position.z);
        }

    }

    //可変フレームレート（Updateより後に実行される）プレイヤーをTranslateで動かすなら
    private void LateUpdate()
    {
        if(updatetype == UpdateType.Late) 
        {
            cameraMove();
        }
    }

    //固定フレームレート(プレイヤーをRigidBodyで移動させるなら)   Time.deltatimeをかけなくていい
    //これ使うとプレイヤーの動きがカクカクしない
    private void FixedUpdate()
    {
        if(updatetype == UpdateType.Fixed)
        {
            cameraMove();
        }
    }

    private void cameraMove()
    {
        var move = playerTf.position - tf.position;
        
        if (playerTf == null)
        {
            move = transform.position;
        }

        //条件演算子　条件式　？　　真の時　：　偽の時
        move.y = moveY ? move.y : 0;
        move.x = moveX ? move.x : 0;
        move.z = 0;   //Z方向に移動して映らなくなるのを防ぐため
                                                      //Lateの時　:  Fixedの時
        float time = updatetype == UpdateType.Late ? Time.deltaTime : 0.032f;
        tf.Translate(move * cameraSpeed * time);
    }

    public enum UpdateType
    { 
        Late,
        Fixed,
    }

}
