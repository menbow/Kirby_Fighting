using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum PlayerorEnemy
//{
//    player,enemy
//}

public class AtackDamage : MonoBehaviour
{
    //�U���̃R���C�_�[�ɕt����

    [Header("���̍U�����G�̍U���������̍U�����ݒ肷��")]
    [SerializeField] private PlayerorEnemy playerorEnemy;
    public PlayerorEnemy GetPlayerorEnemy() => playerorEnemy;

    [SerializeField] int damage = 10;
    public int GetDamage() => damage;
    [SerializeField] float knockback = 3;
    public float GetKnockback() => knockback;

    [Header("���̍U�������������炱��������Ă���X�N���v�g��j�󂷂邩�ݒ肷��")]
    [SerializeField] bool collisionDestroy = false;

    [Header("���̍U���������̃I�u�W�F�N�g�ɓ������Ă��������邩�ǂ����ݒ肷��")]
    [SerializeField] bool roomCollisionIgnore = false;

    ParticleSystem[] destroyEffects;

    bool hitable = false;

    [HideInInspector]public bool hit = false;

    string tagName;

    public string GetTagname()=> tagName;
    
    void Start()
    {
        destroyEffects = GetComponentsInChildren<ParticleSystem>();

        if(playerorEnemy == PlayerorEnemy.player)
        {
            tagName = "Enemy";
        }
        if(playerorEnemy == PlayerorEnemy.enemy)
        {
            tagName = "Player";
        }

        StartCoroutine(Hitable());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitDestroy(collision);
    }
    
    /// <summary>
    /// �q�b�g�����Ƃ��ɔj�󂷂�
    /// </summary>
    /// <param name="collision"></param>
    void HitDestroy(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName) && collisionDestroy && !hit)
        {
            for(int i = 0; i < destroyEffects.Length; i++)
            {

                if(destroyEffects != null)
                {
                    foreach(ParticleSystem effect in destroyEffects)
                    {
                        var e = effect.emission;
                        e.enabled = false;
                    }
                }

            }

            hit = true;
            Destroy(this);
        }

    }

    /// <summary>
    /// �X�e�[�W�Ƃ��ɏՓ˂������ɔj�󂷂�
    /// </summary>
    void StageCollision()
    {
        ////�ђʂ��������Ȃ�collisionDestroy��false�ɂ���
        //if (other.gameObject.CompareTag("Room") && collisionDestroy && hitable && !hit�@&& !roomCollisionIgnore)
        //{
        //    //Debug.Log(other.gameObject);
        //    if (destroyEffects != null)
        //    {
        //        foreach (ParticleSystem effect in destroyEffects)
        //        {
        //            var e = effect.emission;
        //            e.enabled = false;
        //        }
        //    }

        //    hit = true;
        //    Destroy(this);
        //}
    }



    IEnumerator Hitable()
    {
        yield return new WaitForSeconds(0.1f);
        hitable = true;
    }


}
