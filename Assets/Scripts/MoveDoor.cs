using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : MonoBehaviour
{
    public bool open = false;
    public int frames = 0;
    public int maxFrames = 70;

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
        Open();
    }

    void Open()
    {
        if(open) 
        {
            if(frames < maxFrames)
            {
                frames++;
                transform.position += new Vector3(0.2f,0,0);
            }
            else
            {
                open = false;
            }
        }
    }
}
