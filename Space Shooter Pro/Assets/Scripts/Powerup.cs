using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
  
  
  
  [SerializeField] int powerupSpeed = 3;
 // [SerializeField] GameObject playerScriptObj;
  
  //ID for Powerups
  //0 = Triple Shot
  //1 = Speed
  //2 = Shields
						
  [SerializeField] private int powerupID;
  
  
  Player player;
  
  void Start()
  {
  	
	
	
  }
  
  
  void Update()
  {
  	
	transform.Translate(0,-powerupSpeed * Time.deltaTime,0);
	
	if(transform.position.y <= -5.3f)
	{
		Destroy(gameObject);
	}
	
  }
  
  
  
  

  
  private void OnTriggerEnter2D(Collider2D other)
  {
	
	
	if(other.tag == "Player")
	{
		
		player = other.GetComponent<Player>();
		
		if(player != null)
		{
			switch(powerupID)
			{
				case 0: 
					other.GetComponent<Player>().TripleShotActive();
					break;
				case 1:
				if(player.IsSpeedBoostIsActive == false)
				{
					other.GetComponent<Player>().SpeedBoostPowerup();
				}
					break;
				case 2:
				if(player.isShieldActive == false)
				{
					other.GetComponent<Player>().ShieldPowerup();
				}
					break;
				default:
					print("Default");
					break;
				
			}
		
			
		}
		
	
		
		
		
		
		Destroy(gameObject);
	}
	
	
	
  }
  
  
}
