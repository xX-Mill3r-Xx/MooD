using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
     CharacterController controller;

    Vector3 forward, strafe, vertical;
    public float run = 15f;
    public float speedF = 5f;
    public float strafeSpeed = 5f;
    public float gravity = 9.80665f;
    public float jumpSpeed = 3.2f;
    public float maxJumpH = 3f;
    public float timeToMaxH = 0.3f;


    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float speedH = 0.9f;
    private float speedV = 0.9f;

    public static float lifePlayer = 204.6148f;

    void Start()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        gravity = (-2 * maxJumpH) / (timeToMaxH * timeToMaxH);
        jumpSpeed = (2 * maxJumpH) / timeToMaxH;
    }

    void Update()
    {
        Move();
        Head();
    }

    void Move(){
        float fInput = Input.GetAxisRaw("Vertical");
        float sInput = Input.GetAxisRaw("Horizontal");

        forward = fInput * speedF * transform.forward;
        strafe = sInput * strafeSpeed * transform.right;
        vertical += gravity * Time.deltaTime * Vector3.up;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            forward = fInput * (run) * transform.forward;
            strafe = sInput * (run) * transform.right;
        }

        if (controller.isGrounded)
        {
            vertical = Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            vertical = jumpSpeed * Vector3.up;
        }

        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            vertical = Vector3.zero;
        }

        Vector3 finalVelocity = forward + strafe + vertical;
        controller.Move(finalVelocity * Time.deltaTime);
    }

    void Head()
    {
        #region Caso queira movimentar pelo Mouse
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch += speedV * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(-pitch,yaw,0.0f);
        #endregion
        HitEnemy();
        Life();
    }

    void Life()
    {
        RectTransform image = GameObject.Find("Canvas").transform.Find("LifeFull").GetComponent<RectTransform>();

        //image.localPosition = new Vector2(0, 0);

        //lifePlayer -= 0.3f;
        image.sizeDelta = new Vector2(lifePlayer, 27.0681f);
        if(lifePlayer <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void HitEnemy()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
            {
                if (hit.collider.gameObject.tag == "Enemies")
                {
                    GameObject Enemies = hit.collider.gameObject;
                    Enemies.GetComponent<IA>().life--;
                    if (Enemies.GetComponent<IA>().life == 0)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
