using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAction
{
    public EnemyMethod method;
    public float time;
    public float speed;
    public float jumpForce;
    

    /// <summary>
    /// �G���s���s���ƍs�����Ԃ��w�肷��
    /// </summary>
    /// <param name="method">�����̎��</param>
    /// <param name="time">���̓������s������</param>
    public EnemyAction(EnemyMethod method, float time = 2f)
    {
        this.method = method;
        this.time = time;
    }

    public EnemyAction(EnemyMethod method)
    {
        this.method = method;
    }

}
