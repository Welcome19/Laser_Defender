using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject laser;
    public float speed = 15f;
    public float padding = 1f;
    public float laserSpeed;
    public float firingRate = 0.2f;
    public float health = 250f;

    public AudioClip fireSound;

    float xmin;
    float xmax;
	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost= Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f,distance));
        Vector3 rightmost= Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f,distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }
	void Fire()
    {
        Vector3 offset = transform.position+new Vector3(0,1,0);
        GameObject beam = Instantiate(laser, offset, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, firingRate);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-speed*Time.deltaTime, 0, 0);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        //restrict the playspace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        Projectile missile = collider.gameObject.GetComponent<Projectile>();

        if (missile)
        {
            //Debug.Log("player hit by missile");
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                LevelManager man= GameObject.Find("LevelManager").GetComponent<LevelManager>();
                man.LoadLevel("Win");
                Die();
            }
            //Debug.Log("hit by laser");
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
