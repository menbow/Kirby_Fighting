using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutTimeChangeScene : MonoBehaviour
{
    //フェードアウト用のImageコンポーネント
    public Image fadeImage;

    //フェードアウトの時間
    public float fadeTime = 2f;

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
            yield return StartCoroutine(KitamuraMethod.FadeOut(fadeImage,fadeTime));

            //次のシーンに切り替え
            SceneManager.LoadScene(sceneNames[currentSceneIndex]);

            //シーンを更新
            currentSceneIndex++;
        }   
    }
}
