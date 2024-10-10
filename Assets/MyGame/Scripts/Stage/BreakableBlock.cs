using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Atack"))
        {
            //Debug.Log("aaaa");
            Destroy(gameObject);
        }

    }


}
