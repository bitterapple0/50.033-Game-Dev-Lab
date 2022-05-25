using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
	private Rigidbody2D marioBody;
	private float speed = 72;
	private float maxSpeed = 10;
	private float upSpeed = 23;
	private SpriteRenderer marioSprite;
	private bool faceRightState = true;
	private bool onGroundState = true;

	public Transform enemyLocation;
	public Text scoreText;
	private int score = 0;
	private bool countScoreState = false;
	private bool playingState = true;
	public EnemyController enemyScript;
	public MenuController menuScript;
    private Vector2 originalPos;
	// Start is called before the first frame update
	void Start()
	{
		// Set to be 30 FPS
		Application.targetFrameRate =  30;
		marioSprite = GetComponent<SpriteRenderer>();
		marioBody = GetComponent<Rigidbody2D>();
        originalPos = marioBody.position;
		enemyScript = FindObjectOfType<EnemyController>();
        menuScript = FindObjectOfType<MenuController>();
	}


	// Update is called once per frame
	void Update()
	{
        if (playingState){
			if(Input.GetKeyDown("a") && faceRightState){
					faceRightState = false;
					marioSprite.flipX = true;
			}
			if(Input.GetKeyDown("d") && !faceRightState){
					faceRightState = true;
					marioSprite.flipX = false;
			}

			if (!onGroundState && countScoreState)
			{
				if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
				{
						countScoreState = false;
						score++;
				}
			}
        }
	}

	void FixedUpdate()
	{		
		if (playingState) {
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
	
	void OnCollisionEnter2D(Collision2D col)
	{
			if (col.gameObject.CompareTag("Ground")) {
				onGroundState = true;
				countScoreState = false;
				scoreText.text = "Score:" + score.ToString();
			}
	}

	void OnTriggerEnter2D(Collider2D other){
			if(other.gameObject.CompareTag("Enemy")){
				playingState = false;
                menuScript.Restart();
                enemyScript.Stop();
			}
	}

    public void Restart(){
        marioBody.position = originalPos;
        score = 0;
        scoreText.text = "Score:0";
        playingState = true;
    }

}

