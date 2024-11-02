using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyAtacks : MonoBehaviour
{
    
    bool atack = false;
    public bool GetAtacking() => atack;

    bool ButtomHold = false;

    Animator animator;Å@KirbyMove KirbyMove;

    [SerializeField] GameObject jabPrefab; 
    [SerializeField] GameObject hardPunchPrefab; 

    void Start()
    {
        animator = GetComponent<Animator>();
        KirbyMove = GetComponent<KirbyMove>();
    }

    
    void Update()
    {
        if(KirbyMove.GetIsground() && Input.GetButtonDown("Atack") && !atack)
        {
            Atack_Jab();
        }

    }

    void Atack_Jab()
    {
        atack = true;
        StartCoroutine(KoutyokuCoroutine(0.2f));
        Anim_Action("Jab", true);
        LaunchOBJ(jabPrefab);
    }

    void Atack_HardPunch()
    {
        atack = true;
        StartCoroutine(KoutyokuCoroutine(0.5f));

    }


    void LaunchOBJ(GameObject obj)
    {
        GameObject breath = Instantiate(obj, transform.position, Quaternion.identity);
        BreathMove aaa = breath.GetComponent<BreathMove>();

        //Debug.Log(aaa);
        StartCoroutine(aaa.BreathCoroutine(transform.localScale.x));


    }


    IEnumerator KoutyokuCoroutine( float koutyokuTime)
    {
        Debug.Log("fwaefafertiuyyh");
        yield return new WaitForSeconds(koutyokuTime);
        atack = false;
        KirbyMove.Anim_Idle();
    }

    float buttonHoldTime = 0;
    void ButtonHoldCount()
    {
        if (Input.GetButton("Atack"))
        {
            buttonHoldTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Atack"))
        {
            if(buttonHoldTime >= 1f)
            {
                Atack_HardPunch();
            }

            buttonHoldTime = 0;
        }

    }


    void Anim_Action(string animName, bool enforcementPlay = false)
    {
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
        //[0]ÇÕàÍå¬ï™Ç»ÇÃÇ≈îzóÒÇ≈Ç»Ç¢
        string clipName = clipInfo.clip.name;


        if (clipName != animName || enforcementPlay)
        {
            //Debug.Log(clipName);
            animator.Play(animName);
        }
        if (clipName == null)
        {
            animator.Play(animName);
        }
    }


}
