using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutButtonChangeScene : MonoBehaviour
{
    public Image fadeImage;
    public float fadeTime = 2f;
    [SerializeField] string keyButtom;
    [SerializeField] string nextScene;

    void Update()
    {
        if (Input.GetKeyDown(keyButtom))
        {
            StartCoroutine(StartFadeOut());
        }
    }

    IEnumerator StartFadeOut()
    {
        yield return StartCoroutine(KitamuraMethod.FadeOutSceneChange(fadeImage,fadeTime,nextScene));
    }
}
