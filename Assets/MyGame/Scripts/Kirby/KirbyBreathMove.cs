using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StopWatch))]
public class KirbyBreathMove : MonoBehaviour
{
    [SerializeField] float fadeTime = 0.2f;
    [SerializeField] float Speed = 0.2f;

    [HideInInspector]public bool facingRight = true;

    StopWatch sw;

    
    void Start()
    {
        sw = GetComponent<StopWatch>();
    }

    
    void Update()
    {
        float moveSpeed = facingRight ? Speed : Speed * -1;
    }
}
