using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
   [SerializeField] float _enemySpeed = 2f;
   
   [SerializeField] int _enemyHealth = 3;
   
   
  
   
   Player player;
   
   void Start()
   {
	
	
	//player = GameObject.Find("Player").GetComponent<Player>();
		
	
	//todo Check to see if disabled or enabled ??? should not have to though since gameobject will still be in the heirarchy 

	player = GameObject.Find("Player").GetComponent<Player>();
	
	//todo player = GameObject.Find("Player").GetComponent<Player>(); //todo if player != null causes script to not work.

	
   }
   
   
	void Update()
	{
		
		transform.Translate(0,-_enemySpeed * Time.deltaTime,0);

		float enemySpawn = Random.Range(-9,9);
		
		if(transform.position.y <= -5.5f)
		{
			transform.position = new Vector3(enemySpawn,7.50f,0);
		}
		
	}
	
	
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "Player")
		{
		
		
		Destroy(transform.parent.gameObject);
		
		
		
		}
	
	if(other.tag == "Laser")
	{
		DamageEnemy();
		if(player != null)
		{
			player.ScoreFunction(true); //todo add only one point ( should be done ) 
			
		}
		
		
		
	}
	}
	
	
	
	
	void DamageEnemy()
	{
		_enemyHealth--;
		
		
		if(_enemyHealth == 2)
		{
			GetComponent<Renderer>().material.color = Color.yellow;
		}
		
		if(_enemyHealth == 1)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
		
		if(_enemyHealth <= 0)
		{
			if(player != null)
			{
				player.ScoreFunction(false); 
			}
			
		
			Destroy(transform.parent.gameObject); 
			
		}
	}

	
	
	
	
}
