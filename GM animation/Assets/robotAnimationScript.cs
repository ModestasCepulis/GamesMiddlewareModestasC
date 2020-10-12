using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotAnimationScript : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Chicken")
        {
            anim.SetBool("superScared", true);

            Invoke("waitTillAngry", 3f);
        }
    }

    void waitTillAngry()
    {
        anim.SetBool("superScared", false);
        anim.Play("AngryStatus");
        Invoke("waitTillRobotfreakOut", 15f);
    }

    void waitTillRobotfreakOut()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), (new Vector3(4, 4, 5)), 1f);
    }
}
