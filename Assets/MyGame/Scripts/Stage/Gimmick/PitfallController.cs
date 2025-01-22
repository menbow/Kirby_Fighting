using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PitfallController : MonoBehaviour
{
    public string sceneName;

    void Start()
    {
        //現在のシーンを取得
        Scene nowScene = SceneManager.GetActiveScene();

        //現在のシーンの名前を取得
        sceneName = nowScene.name;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
