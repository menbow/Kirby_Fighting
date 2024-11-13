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
        rb.velocity = direction;
    }

    Vector2 direction = Vector2.zero;
    public IEnumerator BreathCoroutine(float kirbyScaleX)
    {
        //Debug.Log("start");

        //âE
        if(kirbyScaleX >= 0)
        {
            direction.x = moveSpeed;
            //rb.velocity = vec;
        }

        //ç∂
        if(kirbyScaleX < 0)
        {
            var a = transform.localScale;
            a.x = kirbyScaleX;
            transform.localScale = a;
            direction.x = -moveSpeed;
            //rb.velocity = vec;
        }

        yield return new WaitForSeconds(destroyTime);

        //Debug.Log("ÇTÇQçsñ⁄");

        if (isCollision)
        {
            yield break;
        }

        if(gameObject != null)
        {
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
