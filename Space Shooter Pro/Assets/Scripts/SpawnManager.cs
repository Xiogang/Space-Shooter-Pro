using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
 
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] GameObject _enemyContainer;
	
	[SerializeField] GameObject[] powerup;
	
	Vector3 powerupSpawn = new Vector3(0,7.22f,0);
	
	[SerializeField] Player player;
	
	
	private bool _stopSpawning = false;
	
	
	int enemyCounter;
 
	void Start()
	{
		
		player = player.GetComponent<Player>(); //todo ----------------------
		
		//!StartCoroutine(SpawnRoutine(3));
		FunctionSpawnRoutine(0.5f);
		StartCoroutine(SpawnPowerupRoutine(3,8));
		
	}
 
	//!spawn game object every 5 seconds
	//!Create a coroutine of type Ienumerator -- yield events
	//while loop 
 
	
	void Update()
	{
		enemyCounter = _enemyContainer.GetComponent<Transform>().transform.childCount;
		
		
		
		
		//Debug.Log(_enemyContainer.GetComponent<Transform>().transform.childCount);
		//_enemyContainer.GetComponent<Transform>().transform.childCount
		
		
	
	
	if(player.playerIsDead == false) //todo check null
	{
		
	
		if(enemyCounter <= 20)
		{
			_stopSpawning = false;
			
		}
		else
		{
			_stopSpawning = true;
			
		}
	}
		
	
		if(enemyCounter <= 0)
		{
			StartCoroutine(SpawnEnemyRoutine(0.5f));
			//todo spawn more powerups for increased waves
			StartCoroutine(SpawnPowerupRoutine(9,12));
		}
		
		//Debug.Log(enemyCounter +" "+_stopSpawning);
		
	}
	
	
	
	
	
	
	
	
	
	
	public void FunctionSpawnRoutine(float x)
	{
		StartCoroutine(SpawnEnemyRoutine(x));
		
	}
	
	
 
	public IEnumerator SpawnEnemyRoutine(float secondsToWait) //! return back to private
	{
		
	
		
		while(_stopSpawning == false) 
		{
			GameObject newEnemy = Instantiate(enemyPrefab, enemyPrefab.transform.position + new Vector3(Random.Range(-8,8),7.10f,0), Quaternion.identity);
			//iterations++; Debug.Log(iterations);
			newEnemy.transform.parent = _enemyContainer.transform;
			yield return new WaitForSeconds(secondsToWait);
		}
		
		

	}




	IEnumerator SpawnPowerupRoutine(int randomSecValue, int randomSecvalue2)
	{
		
		
			while(_stopSpawning == false)
			{	
			
				float powerupSpawnRange = Random.Range(-4.3f, 4.3f);
				int powerupRandomChoice = Random.Range(0,3); //* 
				//Instantiate(TripleShotPowerup, TripleShotPowerup.transform.position = new Vector3(powerupSpawnRange,5.7f,0), Quaternion.identity );
				Instantiate(powerup[powerupRandomChoice], powerupSpawn = new Vector3(powerupSpawnRange,powerupSpawn.y,0), Quaternion.identity);
				yield return new WaitForSeconds(Random.Range(randomSecValue,randomSecvalue2)); //! original 3 and 8 for random.range values
			}
		
	}





	public void OnPlayerDeath()
	{
		_stopSpawning = true;
	}
 
 
}
