using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationOnPlay : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().SetTrigger("StageSelect_Scene");
    }
}
