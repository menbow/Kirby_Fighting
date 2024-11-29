using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_FurinKazan : MonoBehaviour
{
    //親オブジェクト
    public GameObject parentObject;

    //アニメーターコンポーネント
    public Animator animator;

    //再生するアニメーションの名前
    public string animationName;

    void Update()
    {
        //スペースキーが押されたかチェック
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HideParent();
        }
    }

    //親オブジェクトのレンダラーを無効化＋アニメーション再生
    public void HideParent()
    {
        //親オブジェクトのレンダラーを無効にする
        Renderer parentRenderer = parentObject.GetComponent<Renderer>();
        if(parentRenderer != null)
        {
            parentRenderer.enabled = false;
        }
        Debug.Log("アニメーション再生");
        //アニメーションを再生
        animator.Play(animationName);
    }
}
