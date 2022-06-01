using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableMushroom : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D mushroomBody;
    
    private bool hitOnce = false; 
    private int moveRight = 1;
    public Vector2 initalForce = new Vector2(2.3f,2.6f);
    public Vector2 fallingForce = new Vector2(-2f,-1f);
    private Vector2 velocity;

    private float speed = 6;
    private bool onGroundState = false;
    private bool hitPlayer = false;
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        if (mushroomBody.position.x < 0){
            moveRight = 1;
        } else {
            moveRight = -1;
        }
        initalForce.x *= moveRight;
        mushroomBody.AddForce(initalForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(onGroundState && !hitPlayer){
            ComputeVelocity();
            MoveMushroom();
        }

    }

    void MoveMushroom() {
        mushroomBody.MovePosition(mushroomBody.position + velocity * Time.fixedDeltaTime);
    }
    void ComputeVelocity() {
        velocity = new Vector2((moveRight)* speed,0);
    }

    void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle")) {
            onGroundState = true;
        }

        if(col.gameObject.CompareTag("Player")){
            hitPlayer = true;
        }
        
        if (col.gameObject.name == "PipeBtm"){
            moveRight *= -1;
        }

	}

    void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.name != "PipeBtm"){
            onGroundState = false;
            mushroomBody.AddForce(fallingForce, ForceMode2D.Force);
        }
    }
    
}
