using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    [SerializeField]
    //public Animator anim;
    private float speed = 0.09f;
    public float life = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    void Move()
    {
        //Este metodo faz o inimigo sempre olhar para o player
        transform.eulerAngles = new Vector3(GameObject.Find("Main Camera").transform.eulerAngles.x, GameObject.Find("Main Camera").transform.eulerAngles.y, GameObject.Find("Main Camera").transform.eulerAngles.z);
            EnemyStalkPlayer();

    }

    void EnemyStalkPlayer()
    {
        //Este metodo faz o inimigo perseguir o player
        transform.position += -transform.forward * speed;
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "MainCamera") 
        {
            if (Random.Range(0, 100) < 50)
            {
                PlayerMove.lifePlayer--;
            }
        }
    }
}
