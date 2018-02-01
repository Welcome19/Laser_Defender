using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Paddle paddle;
    private Vector3 PaddleToBallVector;
    private bool hasStarted = false;
    private Rigidbody2D rb;
    
    // Use this for initialization
    void Start () {
        paddle = GameObject.FindObjectOfType<Paddle>();
        rb = GetComponent<Rigidbody2D>(); 
        PaddleToBallVector = this.transform.position - paddle.transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasStarted)
        {
            this.transform.position = paddle.transform.position + PaddleToBallVector;
        }
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            this.rb.velocity = new Vector2(1f, 10f);
        }

	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 tweak = new Vector2(Random.Range(0.2f,1f), Random.Range(0.2f, 1f));
        if (hasStarted)
        {
            GetComponent<AudioSource>().Play();
            rb.velocity += tweak;
        }
    }
}
