using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seTest : MonoBehaviour
{
    GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //Debug.Log(gameManager);
        gameManager.AddComponent<SoundsManager>();

    }


    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    SoundsManager.SE_Play(SE.oneUP);
        //}
    }
}
