using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody2D body;
	[SerializeField] float movementSpeed=5f;
	Vector2 direction;
	float horizontal;
	float buttonInput;
	float moveinput;
	
	//AttackPt
	public Transform attackPt;
	public float attackRange=0.7f;
	public LayerMask enemyLayer;
	
	//Animator
	Animator anime;
    // Start is called before the first frame update
    void Start()
    {
	    body= GetComponent<Rigidbody2D>();  
	    anime= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
	{
		
		horizontal=0;
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			horizontal=-1;
		}
		
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			horizontal=1;
		}
		moveinput= horizontal+buttonInput;
		moveinput=Mathf.Clamp(moveinput,-1,1);
		
		//attack
		
		if(Input.GetKeyDown(KeyCode.Z))
		{
			Slash();
		}
		 
		HandleFlip(moveinput);
		
    }
	public	void Slash(){
		anime.SetTrigger("Slash");
		Collider2D[] hits= Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayer);
		
		foreach(Collider2D enemy in hits)
		{
			EnemyAi ei= enemy.GetComponent<EnemyAi>();
			if(ei!= null)
			{
				ei.TakeDamage(1);
			}
		}
	}
	
	void Death()
	{
		anime.SetTrigger("Death");
	}
	void Dash()
	{
		anime.SetTrigger("Dash");
	}
	
	void Hit()
	{
		anime.SetTrigger("Hit");
	}
	void attackRan(){}
	void HandleFlip(float moveinput)
	{
		
		if(moveinput>0)
			transform.localScale= Vector3.one;
			
		else if(moveinput<0)
			transform.localScale= new Vector3(-1,1,1);
	}
    
	void FixedUpdate()
	{
		anime.SetBool("isRunning",moveinput!=0);
		float horizontal= Input.GetAxis("Horizontal");
		
		body.velocity= new Vector2(moveinput* movementSpeed, body.velocity.y);
	}
	
	public void LeftButton()
	{
		buttonInput=-1;
	}
	
	public void RightButton()
	{
		buttonInput=1;
	}
	
	public void stopButton()
	{
		buttonInput=0;
	}
}
