using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
	
	
	
	[SerializeField] private Sprite[] _livesSprite;
	[SerializeField] private Image _LivesImg;
	
	[SerializeField] GameObject Score_text;
	
	[SerializeField] GameObject player_;
	
	[SerializeField] GameObject gameOverText;
	[SerializeField] GameObject restartText;
	
	[SerializeField] private GameManager gameManager;
	
	Player player; //! do not touch
	
	
	void Start()
	{
		
		
		
		//player = GameObject.Find("Player").GetComponent<Player>(); //! do not touch

		
		player = player_.GetComponent<Player>();
		gameManager = gameManager.GetComponent<GameManager>();
		UpdateLives(3);

		if(gameManager == null)
		{
			Debug.Log("Game Manager is null");
		}
	
	
		
	}


	
	float increasedSize = 0.0010f;
	void Update()
	{
		Score_text.GetComponent<TMP_Text>().text = "Score: " + player.scoreTracker;
		
		PlayerLiveImageUpdateFunction();
		
		
		if(player._playerLives == 0)
		{
			gameOverText.SetActive(true);
			StartCoroutine(RestartLevelTextDelayFunction());
			restartText.GetComponent<TMP_Text>().color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(Time.time, 1));
			
			
			gameManager.GameOver();
		
		}
		
	}
	
	



	


	
	IEnumerator RestartLevelTextDelayFunction()
	{
		yield return new WaitForSeconds(1);
		restartText.SetActive(true);
	}
	
	
	
	
	
	
	
	public void PlayerLiveImageUpdateFunction()
	{
		
		switch(player._playerLives)
		{
			case 0:
				UpdateLives(0);
				break;
				
			case 1:
				UpdateLives(1);
				break;
				
			case 2:
				UpdateLives(2);
				break;

		}
		
	}
	
	
	
	public void UpdateLives(int currentLives)
	{
		_LivesImg.sprite = _livesSprite[currentLives];
	}
	
	
}
