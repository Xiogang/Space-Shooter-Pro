using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	[SerializeField] private float _speed = 3.5f;
	
	[SerializeField]
	private float _speedMultipler = 2;
	
	[SerializeField] GameObject _laserPrefab, laserPrefabSpawn;
	
	[SerializeField] float _fireRate = 0.5f;
	float _nextFire = 0f;
	
	
	[SerializeField] public int _playerLives = 3; //todo may need to change back to private
	
	
	Color customColor;
	[SerializeField][Range(0,1)] float _flashSpeed = 0.75f;
	
	private SpawnManager _spawnManager;
	 [SerializeField] GameObject _spawnManagerObj;
	
	
	
	[SerializeField] GameObject tripleShot, shieldVisual; //! delete me???
	
	
	
	public bool isShieldActive, IsTripleShotIsActive, IsSpeedBoostIsActive;
	
	
	 public int scoreTracker;
	
	
	public void Start()
	{
		
		//_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
		
		_spawnManager = _spawnManagerObj.GetComponent<SpawnManager>();
		
		
		
		//!_spawnManager = _spawnManagerObj.GetComponent<SpawnManager>();
	

		
		
		if(_spawnManager == null)
		{
			Debug.LogError("The Spawn Manager is null");
		}
		
		//transform.position = new Vector3(0,0,0);
		customColor = new Color(0.000f, 0.519f, 1.000f, 1.000f);
		
	}

	
	
	
	
	void Update()
	{
		PlayerTeleportAndBound();
		DamageFlashAlert();
		
		

		if(Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
		{
			FiringLaser();

		}
		
		//!KeySwitchFunction(); // for testing purposes only
		
	
		
	}
	
	
	void FiringLaser()
	{

		_nextFire = Time.time + _fireRate;
		Instantiate(_laserPrefab, transform.position + new Vector3(0,0.85f,0), Quaternion.identity);
	
		if(IsTripleShotIsActive == true)
		{
			Instantiate(tripleShot, transform.transform.position, Quaternion.identity);	
		}		

		
	}
	
	
	bool isOn;
	void KeySwitchFunction()
	{

		if(Input.GetKeyDown(KeyCode.LeftShift) && isOn == false  )
		{
			Debug.Log(isOn);
			StartCoroutine(DelayButtonSwitchOn());
			
		}
		
		if(Input.GetKeyDown(KeyCode.LeftShift) && isOn == true  )
		{
			Debug.Log(isOn);
			StartCoroutine(DelayButtonSwitchOff());
		}
	
	}
	
	
	IEnumerator DelayButtonSwitchOn()
	{
		yield return new WaitForSeconds(0.05f);
		isOn = true;	
	}
	
	IEnumerator DelayButtonSwitchOff()
	{
		yield return new WaitForSeconds(0.05f);
		isOn = false;	
	}
	
	
	

	void ControllerFunction(float horizontalInput, float verticalInput)    
	{
		
	
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(horizontalInput, verticalInput,0);
		
		if(IsSpeedBoostIsActive == false)
		{
			transform.Translate(direction * _speed * Time.deltaTime);
		}
		else if(IsSpeedBoostIsActive == true)
		{
			transform.Translate(direction * (_speed * _speedMultipler) * Time.deltaTime);
		}
		
		
		
		

	}
	
	void PlayerTeleportAndBound()
	{
		
		ControllerFunction(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
		

		if(transform.position.x >= 11.29f)
		{
			transform.position = new Vector3(-11.29f,transform.position.y,0);
		}
		else if(transform.position.x <= -11.29f)
		{
			transform.position = new Vector3(11.29f, transform.position.y, 0);
		}


		//!transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0),0); //& transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0),0);

		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.7f, 5.5f),0);
	}
	
	
	
	
	private void OnTriggerEnter2D(Collider2D other)
	{
			if(other.tag == "Enemy")
		{
		
			DamagePlayer();
		}
	}
	
	

	
	
													public bool playerIsDead = false;
	public void DamagePlayer() //&modified
	{
		
		if(isShieldActive == true)
		{
			isShieldActive = false; 
			shieldVisualMovement.SetActive(false);
			return;
		}
		
		
		_playerLives--;
		
			if(_playerLives <= 0)
			{
				_spawnManager.OnPlayerDeath();
				//gameObject.SetActive(false);
				playerIsDead = true;
				Destroy(gameObject); //todo glitch
				
			}
		
		

	}

	public void DamageFlashAlert()
	{
		if(_playerLives == 1)
		{
			GetComponent<Renderer>().material.color = Color.Lerp(customColor, Color.red, Mathf.PingPong(Time.time, _flashSpeed));
		}

	}

	
	
	
	public void TripleShotActive()
	{
		IsTripleShotIsActive = true; 
		StartCoroutine(TripleShotPowerDown(5));
		
	}
	
	
	IEnumerator TripleShotPowerDown(float powerupSeconds)
	{
		
		yield return new WaitForSeconds(powerupSeconds);
		IsTripleShotIsActive = false;
	}
	
	
	
	
	public void SpeedBoostPowerup()          
	{
		if(IsSpeedBoostIsActive == false) //* new test
		{
			_speed = _speed*_speedMultipler; //! delete
			IsSpeedBoostIsActive = true;
		}
		//_speed = _speed*_speedMultipler; //! delete
		StartCoroutine(SpeedBoostPowerDown());
		
		
	}
	
	IEnumerator SpeedBoostPowerDown()
	{
		yield return new WaitForSeconds(5);
		IsSpeedBoostIsActive = false;
		_speed = _speed/ _speedMultipler; //!
		
	}
	
	
	GameObject shieldVisualMovement;
	public void ShieldPowerup() //* 
	{
		isShieldActive = true; 
		
		shieldVisualMovement = Instantiate(shieldVisual, transform.position, Quaternion.identity);
		shieldVisualMovement.GetComponent<Transform>().SetParent(transform);
		//isShieldActive = true;
	}
	

	
	
	public void ScoreFunction(bool perDamage) //* new modifying 
	{
		
		if(perDamage == true)
		{
			scoreTracker++;  //todo 
		}
		
		if(perDamage == false)
		{
			scoreTracker += 10; //todo
		}
		
	}
	
	
	
	
	
	
	
	//todo method to add 10 to the score
	//todo communicate with the UI to update score
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	void PlayerStopAtBound()
	{
		ControllerFunction(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		
		if(transform.position.x >= 9.1f)
		{
			transform.position = new Vector3(9.1f, transform.position.y, transform.position.z);
		}
		else if(transform.position.x <= -9.1f)
		{
			transform.position = new Vector3(-9.1f,transform.position.y,transform.position.z);
		}
		
		
		if(transform.position.y >= 5.9f)
		{ 
			transform.position = new Vector3(transform.position.x,5.9f,transform.position.z);
		}
		else if(transform.position.y <= -3.8f)
		{
			transform.position = new Vector3(transform.position.x,-3.8f,transform.position.z);
		}
		
		
		
		//^ x = 9.1f       -x = -9.1f
		//^ y = 5.9f        y = -3.8 
		
		
	}

	void PlayerOutBoundesTeleport()
	{
		
			ControllerFunction(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			

		if(transform.position.x >= 9.1f)
		{
			transform.position = new Vector3(-9.1f,transform.position.y,transform.position.z);
			ControllerFunction(0,Input.GetAxis("Vertical"));
			
		}
		else if(transform.position.x <= -9.1f)
		{
			transform.position = new Vector3(9.1f,transform.position.y,transform.position.z);
		}

		if(transform.position.y >= 5.9f)
		{
			transform.position = new Vector3(transform.position.x,-3.8f,transform.position.z);
		}
		else if(transform.position.y <= -3.8f)
		{
			transform.position = new Vector3(transform.position.x,5.9f,transform.position.z);
		}
	
		//^ x = 9.1f       -x = -9.1f
		//^ y = 5.9f        y = -3.8 
		
		
	}
	

	
}
