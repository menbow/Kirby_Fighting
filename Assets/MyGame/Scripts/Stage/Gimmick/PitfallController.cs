using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PitfallController : MonoBehaviour
{
    public string sceneName;

    //上に飛ばす力
    public float upPower = 10f;

    //下に動く範囲
    public float downHeight = 2f;

    //フェードアウト用のImageコンポーネント
    public Image fadeImage;

    //フェードアウトの時間
    public float fadeTime = 2f;

    //プレイヤー
    public GameObject playerObject;

    //ゲームオーバーオブジェクト
    public GameObject gameOverObject;

    private Rigidbody2D gameOverRigidBody2D;

    void Start()
    {
        //現在のシーンを取得
        Scene nowScene = SceneManager.GetActiveScene();

        //現在のシーンの名前を取得
        sceneName = nowScene.name;

        gameOverRigidBody2D = gameOverObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (gameOverObject.transform.position.y < downHeight)
        {
            gameOverObject.SetActive(false);
            StartCoroutine(KitamuraMethod.FadeOutSceneChange(fadeImage, fadeTime, sceneName));
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            gameOverObject.SetActive(true);
            playerObject.SetActive(false);
            gameOverObject.transform.position = playerObject.transform.position;
            gameOverRigidBody2D.AddForce(Vector2.up * upPower, ForceMode2D.Impulse);
        }
    }
}