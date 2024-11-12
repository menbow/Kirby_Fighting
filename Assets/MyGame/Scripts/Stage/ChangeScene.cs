using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //�t�F�[�h�A�E�g�p��Image�R���|�[�l���g
    public Image fadeImage;

    //�t�F�[�h�A�E�g�̎���
    public float fadeDuration = 2f;

    //�؂�ւ���V�[���̖��O�̔z��
    public string[] sceneNames;

    //�V�[�����X�V���邽�߂̕ϐ�
    private int currentSceneIndex = 0;

    void Start()
    {
        //3�b��ɃV�[����؂�ւ���R���[�`���J�n
        StartCoroutine(SwitchSceneAfterDelay(3f));
    }

    IEnumerator SwitchSceneAfterDelay(float delay)
    {
        while (currentSceneIndex < sceneNames.Length)
        {
            //�w�肳�ꂽ�b���ҋ@
            yield return new WaitForSeconds(delay);

            //�t�F�[�h�A�E�g���J�n
            yield return StartCoroutine(FadeOut());

            //���̃V�[���ɐ؂�ւ�
            SceneManager.LoadScene(sceneNames[currentSceneIndex]);

            //�V�[�����X�V
            currentSceneIndex++;
        }   
    }

    IEnumerator FadeOut()
    {
        //�o�ߎ���
        float elapsedTime = 0f;

        //�t�F�[�h�A�E�g������Image�̐F���擾
        Color color = fadeImage.color;

        //�o�ߎ��Ԃ��t�F�[�h�A�E�g���ԂɒB����܂Ń��[�v
        while(elapsedTime < fadeDuration)
        {
            //�o�ߎ��Ԃ̍X�V
            elapsedTime += Time.deltaTime;

            //Alpha�l(�����x)���X�V
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);

            //�X�V���ꂽ�F��Image�ɓK�p
            fadeImage.color = color;

            //���̃t���[���܂ňꎞ��~
            yield return null; 
        }
    }
}
