using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField] float _laserSpeed = 8;

	// Update is called once per frame
	void Update()
	{
		transform.Translate(0,_laserSpeed*Time.deltaTime,0);
		
		
		if(transform.position.y >= 7)
		{
			//check if this object has a parent
			//destroy the parent too
			
			if(transform.parent != null)
			{
				Destroy(transform.parent.gameObject);
			}
		
		
			
			
			Destroy(this.gameObject);
		}
		
		
	}
	
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		
		
		
		
		
		if(other.tag == "Enemy")
		{
			Destroy(gameObject);
		}
	}
	
	
	

}
