using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenController : MonoBehaviour
{
    public Animator anim;

    float randomNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            randomNumber = Random.Range(1, 3);

            if(randomNumber == 1)
            {
                anim.Play("Run");
            }
            if(randomNumber == 2)
            {
                anim.Play("Peck");
                Invoke("chickenFreakOut", 20f);
            }

        }
    }

    void chickenFreakOut()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), (new Vector3(4, 4, 5)), 1f);
    }

}
