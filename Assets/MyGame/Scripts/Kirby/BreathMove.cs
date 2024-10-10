using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreathMove : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2f;

    [SerializeField] float destroyTime = 0.3f;

    Rigidbody2D rb;

    bool isCollision = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        rb.velocity = vec;
    }

    Vector3 vec = Vector3.zero;
    public IEnumerator BreathCoroutine(float kirbyScaleX)
    {

        //âE
        if(kirbyScaleX >= 0)
        {
            vec.x = moveSpeed;
            //rb.velocity = vec;
        }

        //ç∂
        if(kirbyScaleX < 0)
        {
            var a = transform.localScale;
            a.x = kirbyScaleX;
            transform.localScale = a;
            vec.x = -moveSpeed;
            //rb.velocity = vec;
        }

        if (isCollision)
        {
            yield break;
        }
        else if(gameObject != null)
        {
            yield return new WaitForSeconds(destroyTime);
            //Debug.Log("breath");
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBroke"))
        {
            isCollision = true;
            Destroy(gameObject);
        }
    }

}
