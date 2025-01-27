using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInTimeChangeScene : MonoBehaviour
{
    //フェードアウト用のImageコンポーネント
    public Image fadeImage;

    //フェードアウトの時間
    public float fadeTime = 2f;

    void Start()
    {
        StartCoroutine(StartFadeOut());
    }

    IEnumerator StartFadeOut()
    {
        yield return StartCoroutine(FadeOut());
    }

    //フェードアウト処理
    IEnumerator FadeOut()
    {
        //経過時間
        float elapsedTime = 0;

        //フェードアウトさせるImageの色を取得
        Color fadeColor = fadeImage.color;

        //経過時間がフェードアウト時間に達するまでループ
        while (elapsedTime < fadeTime)
        {
            //経過時間の更新
            elapsedTime += Time.deltaTime;

            //Alpha値(透明度)を更新
            fadeColor.a = Mathf.Clamp01(1.0f - (elapsedTime / fadeTime));

            //更新された色をImageに適用
            fadeImage.color = fadeColor;

            //次のフレームまで一時停止
            yield return null;
        }
    }

}
