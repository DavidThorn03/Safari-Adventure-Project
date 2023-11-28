using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private GameManager gm;
    public float speed;
    private float lowerBounds = -10;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isGameActive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (transform.position.z < lowerBounds)
        {
            Destroy(gameObject);
        }
    }
}
