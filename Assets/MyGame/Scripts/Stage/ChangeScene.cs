using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //フェードアウト用のImageコンポーネント
    public Image fadeImage;

    //フェードアウトの時間
    public float fadeDuration = 2f;

    //切り替えるシーンの名前の配列
    public string[] sceneNames;

    //シーンを更新するための変数
    private int currentSceneIndex = 0;

    void Start()
    {
        //3秒後にシーンを切り替えるコルーチン開始
        StartCoroutine(SwitchSceneAfterDelay(3f));
    }

    IEnumerator SwitchSceneAfterDelay(float delay)
    {
        while (currentSceneIndex < sceneNames.Length)
        {
            //指定された秒数待機
            yield return new WaitForSeconds(delay);

            //フェードアウトを開始
            yield return StartCoroutine(FadeOut());

            //次のシーンに切り替え
            SceneManager.LoadScene(sceneNames[currentSceneIndex]);

            //シーンを更新
            currentSceneIndex++;
        }   
    }

    IEnumerator FadeOut()
    {
        //経過時間
        float elapsedTime = 0f;

        //フェードアウトさせるImageの色を取得
        Color color = fadeImage.color;

        //経過時間がフェードアウト時間に達するまでループ
        while(elapsedTime < fadeDuration)
        {
            //経過時間の更新
            elapsedTime += Time.deltaTime;

            //Alpha値(透明度)を更新
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);

            //更新された色をImageに適用
            fadeImage.color = color;

            //次のフレームまで一時停止
            yield return null; 
        }
    }
}
