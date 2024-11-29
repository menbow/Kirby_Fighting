using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_FurinKazan : MonoBehaviour
{
    //�e�I�u�W�F�N�g
    public GameObject parentObject;

    //�A�j���[�^�[�R���|�[�l���g
    public Animator animator;

    //�Đ�����A�j���[�V�����̖��O
    public string animationName;

    void Update()
    {
        //�X�y�[�X�L�[�������ꂽ���`�F�b�N
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HideParent();
        }
    }

    //�e�I�u�W�F�N�g�̃����_���[�𖳌����{�A�j���[�V�����Đ�
    public void HideParent()
    {
        //�e�I�u�W�F�N�g�̃����_���[�𖳌��ɂ���
        Renderer parentRenderer = parentObject.GetComponent<Renderer>();
        if(parentRenderer != null)
        {
            parentRenderer.enabled = false;
        }
        Debug.Log("�A�j���[�V�����Đ�");
        //�A�j���[�V�������Đ�
        animator.Play(animationName);
    }
}
