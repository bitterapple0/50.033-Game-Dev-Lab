using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
	private Rigidbody2D marioBody;
	public float speed = 150;
	public float maxSpeed = 5;
	public float upSpeed = 25;
	private SpriteRenderer marioSprite;
	private bool faceRightState = true;
	private bool onGroundState = true;

	public Transform enemyLocation;
	public Text scoreText;
	private int score = 0;
	private bool countScoreState = false;
	public bool restartFlag = false;
	public EnemyController enemyScript;
	public MenuController menuScript;
    private Vector2 originalPos;
	private AudioSource marioAudio;
	private Animator marioAnimator;
	// Start is called before the first frame update
	void Start()
	{
		// Set to be 30 FPS
		Application.targetFrameRate =  60;
		marioSprite = GetComponent<SpriteRenderer>();
		marioBody = GetComponent<Rigidbody2D>();
        originalPos = marioBody.position;
		enemyScript = FindObjectOfType<EnemyController>();
        menuScript = FindObjectOfType<MenuController>();
		marioAnimator  =  GetComponent<Animator>();
		marioAudio = GetComponent<AudioSource>();
	}


	// Update is called once per frame
	void Update()
	{
        if (!restartFlag){
			if(Input.GetKeyDown("a") && faceRightState){
					faceRightState = false;
					marioSprite.flipX = true;
					if (Mathf.Abs(marioBody.velocity.x) >  1.0) marioAnimator.SetTrigger("onSkid");
        	}
			}
			if(Input.GetKeyDown("d") && !faceRightState){
					faceRightState = true;
					marioSprite.flipX = false;
					if (Mathf.Abs(marioBody.velocity.x) >  1.0) marioAnimator.SetTrigger("onSkid");
			}

			if (!onGroundState && countScoreState)
			{
				if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
				{
						countScoreState = false;
						score++;
				}
			}
			
			marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
			marioAnimator.SetBool("onGround", onGroundState);
	}

	void FixedUpdate()
	{		
		if (!restartFlag) {
			float moveHorizontal = Input.GetAxis("Horizontal");
			if (Mathf.Abs(moveHorizontal)> 0){
				Vector2 movement = new Vector2(moveHorizontal, 0);
					if(marioBody.velocity.magnitude < maxSpeed) marioBody.AddForce(movement * speed);
			}
			if((Input.GetKeyUp("a") || Input.GetKeyUp("b")) && !onGroundState){
						marioBody.velocity = Vector2.zero;
			}
			if (Input.GetKeyDown("space") && onGroundState){
				marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
				onGroundState = false;
				countScoreState = true;
			}
        }
	}
	
	void  PlayJumpSound(){	
		marioAudio.PlayOneShot(marioAudio.clip);
	}	
	void OnCollisionEnter2D(Collision2D col)
	{
			if (col.gameObject.CompareTag("Ground")) {
				onGroundState = true;
				countScoreState = false;
				// scoreText.text = "Score:" + score.ToString();
				marioAnimator.SetBool("onGround", onGroundState);
			}

			if (col.gameObject.CompareTag("Obstacle") && Mathf.Abs(marioBody.velocity.y)<0.01f){
				onGroundState = true;
				marioAnimator.SetBool("onGround", onGroundState);

			}
	}

	// void OnTriggerEnter2D(Collider2D other){
	// 		if(other.gameObject.CompareTag("Enemy")){
	// 			restartFlag = true;
    //             menuScript.Restart();
    //             enemyScript.Stop();
	// 		}
	// }

}

