using UnityEngine;
using System.Collections;

public class IADogResting : IADogStates

{
	private readonly IADogStatePattern enemy;


	public IADogResting (IADogStatePattern iaDogStatePattern)
	{
		enemy = iaDogStatePattern;
	}

	public void UpdateState()
	{
		Rest ();
	}

	public void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			enemy.close = true;
			enemy.target = other.transform;
		}
	}

	public void ToIADogResting()
	{
		enemy.currentState = enemy.iaDogResting;
	}

	public void ToIADogFollowing()
	{
		enemy.currentState = enemy.iaDogFollowing;
	}

	private void Rest ()
	{
		if (enemy.close == true){
			ToIADogFollowing();
		}
	}
}
