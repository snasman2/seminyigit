using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//k?t?phaneler

public class player : MonoBehaviour
{
    Rigidbody2D rb;
    float xinput;
    public float speed;
    public Animator animator;
    bool facing = true;
    bool face = true;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //fizik ile u?ra?may? sa?l?yor
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (facing)
            {
                rb.AddForce(new Vector2(10000, 0f));
            }
            if (!facing)
            {
                rb.AddForce(new Vector2(-10000, 0f));
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScale *= -1;
            flip_vertical();
        }
        //space ile u?



        xinput = Input.GetAxisRaw("Horizontal");
        //-1, 1 aras? de?er al?yor  

        rb.AddForce(new Vector2(xinput * speed, 0f));
        //sphere objesiinin h?z de?eri il ?stte ald??? de?eri ?arparak ok tu?lar? ile
        //sa?-sol gitmeyi sa?l?yor (hala ne yap?yor ?stteki bilmiyorum)

        animator.SetFloat("speed", Mathf.Abs(xinput));

        if (xinput < 0 && facing)
        {
            flip_horizontal();
        }
        if (xinput > 0 && !facing)
        {
            flip_horizontal();
        }
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
        //karaktere (kareye) t?kland???nda yok ediyor
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
        }
        //enemy (di?er kare) ile temas adip edmedi?ini kontrol ediyor, e?er ediyor ise
        //yazan komutlar??al??t?r?yor (karakteri yok ediyor bu durumda)

        if (collision.gameObject.tag == "finish")
        {
            SceneManager.LoadScene("level 2");
        }


        if (collision.gameObject.tag == "platform")
        {
            animator.SetBool("touching_ground", true);
        }

        if (collision.gameObject.tag != "platform")
        {
            animator.SetBool("touching_ground", false);
        }

    }
    void flip_horizontal()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facing = !facing;
    }
    void flip_vertical()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.y *= -1;
        gameObject.transform.localScale = currentScale;
        face = !face;
    }
}
