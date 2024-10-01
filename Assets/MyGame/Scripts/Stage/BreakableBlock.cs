using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    BreakableBlock[] brocks;

    int touchBrock = 0;

    void Start()
    {
        brocks = GameObject.FindObjectsOfType<BreakableBlock>();

        foreach (BreakableBlock b in brocks)
        {
            Debug.Log(Vector2.Distance(transform.position, b.transform.position));

            if(Mathf.Abs(Vector2.Distance(transform.position, b.transform.position)) < 1.6f)
            {
                touchBrock++;
            }
        }
        Debug.Log(touchBrock);
    }

    
    void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("exit");
    }

    IEnumerator BreakCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
