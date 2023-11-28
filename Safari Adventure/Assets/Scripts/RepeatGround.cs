using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatGround : MonoBehaviour
{
    private Vector3 startpos;
    private float repeat;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        repeat = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < startpos.z - repeat){
            transform.position = startpos;
        }
    }
}
