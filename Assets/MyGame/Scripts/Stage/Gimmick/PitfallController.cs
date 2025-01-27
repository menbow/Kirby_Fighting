using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PitfallController : MonoBehaviour
{
    public string sceneName;

    //上に動く速度
    public float upSpeed = 1f;

    //上に動く範囲
    public float upHeight = 2f;

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

    void Start()
    {
        //現在のシーンを取得
        Scene nowScene = SceneManager.GetActiveScene();

        //現在のシーンの名前を取得
        sceneName = nowScene.name;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            gameOverObject.SetActive(true);
            playerObject.SetActive(false);
            StartCoroutine(GameOverController());
        }
    }

    /// <summary>
    /// ゲームオーバーアニメーションコントロール
    /// </summary>
    /// <returns></returns>
    IEnumerator GameOverController()
    {
        //ゲームオーバー座標を取得
        Vector2 gameOverPosition = playerObject.transform.position;

        //ゲームオーバーオブジェクトの表示
        gameOverObject.transform.position = gameOverPosition;

        //上に動く
        while (gameOverObject.transform.position.y < gameOverPosition.y + upHeight)
        {
            gameOverObject.transform.position += Vector3.up * upSpeed * Time.deltaTime;
            yield return null;
        }

        //下に動く
        while (gameOverObject.transform.position.y > gameOverPosition.y + downHeight)
        {
            gameOverObject.transform.position += Vector3.down * upSpeed * Time.deltaTime;
            yield return null;
        }
        gameOverObject.SetActive(false);

        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// フェードアウト処理
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        //経過時間
        float elapsedTime = 0f;

        //フェードアウトさせるImageの色を取得
        Color fadeColor = fadeImage.color;

        //経過時間がフェードアウト時間に達するまでループ
        while (elapsedTime < fadeTime)
        {
            //経過時間の更新
            elapsedTime += Time.deltaTime;

            //Alpha値(透明度)を更新
            fadeColor.a = Mathf.Clamp01(elapsedTime / fadeTime);

            //更新された色をImageに適用
            fadeImage.color = fadeColor;

            //次のフレームまで一時停止
            yield return null;
        }
    }
}