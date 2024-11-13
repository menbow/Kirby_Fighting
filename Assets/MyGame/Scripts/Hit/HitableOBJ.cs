using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerorEnemy
{
    player,enemy
}
public class HitableOBJ : MonoBehaviour
{
    [SerializeField, Header("�����Ɏ��g���v���C���[���G������͂���")]
    PlayerorEnemy playerorEnemy = PlayerorEnemy.player;
    [SerializeField] int maxHP = 100;
    [SerializeField] float invincibleTime = 1f;

    [SerializeField] GameObject hitEffect;

    [SerializeField] bool debugLog = false;

    bool damageable = true;
    int currentHP = 0;
    string tagName;
    PlayerorEnemy getAtackcol;

    public int GetHP() => currentHP;
    public int GetMaxHP() => maxHP;

    bool death = false;
    public bool GetDeath() => death;

    /// <summary> �������U���Ƀq�b�g���ăm�b�N�o�b�N���Ă�����true��Ԃ� </summary>
    bool ishit = false;
    public bool GetHit() => ishit;

    Rigidbody rb;



    void Start()
    {
        damageable = true;
        rb = GetComponent<Rigidbody>();
        currentHP = maxHP;

        if (playerorEnemy == PlayerorEnemy.player) { tagName = "EnemyAtack"; getAtackcol = PlayerorEnemy.enemy; }
        if (playerorEnemy == PlayerorEnemy.enemy) { tagName = "PlayerAtack"; getAtackcol = PlayerorEnemy.player; }
    }


    void Update()
    {
        //Debug.Log(rb.velocity);

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if (debugLog)
        {
            Debug.Log(currentHP);
            Debug.Log(tagName);
        }

        if (currentHP <= 0)
        {
            //Debug.Log("���ɂ܂���");
            death = true;
            rb.velocity = Vector3.zero;
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    AtackDamage AtackCol = other.GetComponent<AtackDamage>();

    //    //�G�̍U���̃R���C�_�[�ɐڐG�����Ƃ�
    //    if (other.gameObject.CompareTag(tagName) && AtackCol != null)
    //    {
    //        //�����������̂��G�̂��̂������ꍇ
    //        if (AtackCol.GetPlayerorEnemy() == getAtackcol && damageable)
    //        {
    //            //�m�b�N�o�b�N����
    //            Vector3 dir = other.transform.position - transform.position;
    //            dir.y = 0;
    //            var aaa = KitamuraMethod.VectorReplaced(transform.forward, AtackCol.GetKnockback());
    //            //Debug.Log(aaa);
    //            rb.AddForce(-aaa * 10, ForceMode.Impulse);
    //            //StartCoroutine(KnockBackCoroutine(aaa));

    //            //�_���[�W����
    //            if (AtackCol != null)
    //            {
    //                currentHP -= AtackCol.GetDamage();
    //                //Debug.Log(currentHP);
    //                damageable = false;
    //                StartCoroutine(DamageFragCoroutine());
    //                HitEffect(other.transform.position);
    //            }
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }




    IEnumerator KnockBackCoroutine(Vector3 vector)
    {
        ishit = true;
        vector.y = 0;
        //rb.velocity += vector * 5;
        rb.AddForce(vector * 5);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector3.zero;
        ishit = false;
    }

    IEnumerator DamageFragCoroutine()
    {
        yield return new WaitForSeconds(invincibleTime);
        damageable = true;
    }

    public void Healing(int hp)
    {
        currentHP += hp;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        //Debug.Log(currentHP);
    }

    public void ForcedDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0)
        {
            currentHP = 0;
        }
    }

    /// <summary> �q�b�g�G�t�F�N�g�𐶐����� null�`�F�b�N������Ă� </summary>
    void HitEffect(Vector3 pos)
    {
        if (hitEffect != null && currentHP > 0)
        {
            Instantiate(hitEffect, pos, transform.rotation);
        }
    }

}
