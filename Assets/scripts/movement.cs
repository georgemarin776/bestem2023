using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class movement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()    
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
          body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
    }
}
