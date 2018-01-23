using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speedX = 2f;
	public float jumpForce = 350f;
    private Rigidbody2D rb2d;

    public bool grounded;
    public Transform groundcheck;
    private float groundRadius = 0.25f;
    public LayerMask whatIsGround;

	public bool jumped; 

	public AudioSource JumpSound;

	private Animator anim; 

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
    }

    // Use this for initialization
    void FixedUpdate () {
        grounded = Physics2D.OverlapCircle(groundcheck.position, groundRadius, whatIsGround);
	}
	
	// Update is called once per frame
	void Update () {

        // Beveger spilleren venstre og høyre
        var x = Input.GetAxis ("Horizontal") * Time.deltaTime * speedX;
        transform.Translate(x, 0, 0);

        // Hopp
        if (Input.GetButtonDown("Jump") && grounded) {
            rb2d.AddForce(Vector2.up * jumpForce);
            grounded = false;
			JumpSound.Play ();
			anim.SetTrigger ("Jump"); 
			jumped = true;

        }

		if (grounded && jumped) 
		{
			anim.SetTrigger ("Land"); 
			jumped = false; 
		}
	}
}