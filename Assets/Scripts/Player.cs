using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;
    private string WALK_ANIMATION = "Walk";

    private bool jumping = false;

    private bool isGrounded;
    private string GROUND_TAG = "Ground";

    private string ENEMY_TAG = "Enemy";

    private void Awake() {

        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

	}


	// Start is called before the first frame update
	void Start()
    {
        
    }



	// Update is called once per frame
	void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) jumping = true;
        
        
        PlayerMoveKeyboard();
        AnimatePlayer();

    }

	private void FixedUpdate() {
        
        if(jumping) {
            
            PlayerJump();

		}

    }

    void PlayerMoveKeyboard() {


        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += moveForce * Time.deltaTime * new Vector3(movementX, 0f, 0f);
	}

    void AnimatePlayer() {

        // going to the right
        if(movementX > 0) {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
		}

        // going to the left
        else if(movementX < 0) {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }

        // idle
        else {
            anim.SetBool(WALK_ANIMATION, false);
        }


	}

    void PlayerJump() {
        
        myBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumping = false;
        isGrounded = false;

    }

	private void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.CompareTag(GROUND_TAG)) {

            isGrounded = true;

		}

        if(collision.gameObject.CompareTag(ENEMY_TAG)) {

            Destroy(gameObject);

		}

	}

	private void OnTriggerEnter2D(Collider2D collision) {
		
        if(collision.CompareTag(ENEMY_TAG)) {
            Destroy(gameObject);
		}

	}




} // class
