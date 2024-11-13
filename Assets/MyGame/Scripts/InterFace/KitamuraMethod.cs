using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class KitamuraMethod 
{


    /// <summary>
    /// ������Vecter3��������ς����ɒl��u��������
    /// </summary>
    /// <param name="dir">�x�N�g��</param>
    /// <param name="xValue"></param>
    /// <param name="zValue"></param>
    /// <returns>�u����������x�N�g�������K�����ꂽ�l</returns>
    public static Vector3 VectorReplaced(Vector3 dir, float xValue, float zValue)
    {
        Vector3 vector3 = dir;
        if (dir.x > 0) { vector3.x = xValue; }
        if (dir.x < 0) { vector3.x = -xValue; }
        if (dir.z > 0) { vector3.z = zValue; }
        if (dir.z < 0) { vector3.z = -zValue; }

        return vector3.normalized;
    }

    /// <summary>
    /// ������Vecter3��������ς����ɒl��u��������
    /// </summary>
    /// /// <returns>�u����������x�N�g�������K�����ꂽ�l</returns>

    public static Vector3 VectorReplaced(Vector3 dir, float Value)
    {
        Vector3 vector3 = dir;
        if (dir.x > 0) { vector3.x = Value; }
        if (dir.x < 0) { vector3.x = -Value; }
        if (dir.z > 0) { vector3.z = Value; }
        if (dir.z < 0) { vector3.z = -Value; }

        return vector3.normalized;
    }


    /// <summary>
    /// ��_�̕����̃x�N�g�������߂鐳�K�������܂��B
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="goalPos"></param>
    /// <returns></returns>
    public static Vector3 DirCalculation( Vector3 startPos,Vector3 goalPos )
    {
        Vector3 dir = goalPos - startPos;
        return dir.normalized;
    }


    /// <summary>
    /// �����̒l��0�ȉ��ɂ����Ȃ��悤�ɂ���
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float NotbelowZero( float value )
    {
        //Mathf.Max(value, 0);
        if (value <= 0)
        {
            return 0;
        }

        return value;
    }


    public static GameObject FindPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// �V�[����ǂݍ��݂܂�
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene( string sceneName ,float second)
    {
        //StartCoroutine(LoadSceneCoroutine(sceneName, second));
    }

    public static IEnumerator LoadSceneCoroutine(string sceneName, float sec)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(sceneName);
    }


    /// <summary>
    /// ���C���[��ݒ肷��
    /// </summary>
    /// <param name="needSetChildrens">�q�ɂ����C���[�ݒ���s����</param>
    public static void SetLayer(this GameObject gameObject, int layerNo, bool needSetChildrens = true)
    {
        if (gameObject == null)
        {
            return;
        }
        gameObject.layer = layerNo;

        //�q�ɐݒ肷��K�v���Ȃ��ꍇ�͂����ŏI��
        if (!needSetChildrens)
        {
            return;
        }

        //�q�̃��C���[�ɂ��ݒ肷��
        foreach (Transform childTransform in gameObject.transform)
        {
            SetLayer(childTransform.gameObject, layerNo, needSetChildrens);
        }
    }

}
