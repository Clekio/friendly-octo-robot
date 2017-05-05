using UnityEngine;
using System.Collections;

public class IADogFollowing : IADogStates

{
	private readonly IADogStatePattern enemy;

	public IADogFollowing (IADogStatePattern iaDogStatePattern)
	{
		enemy = iaDogStatePattern;
	}

	public void UpdateState()
	{
		Follow ();
	}

	public void OnTriggerEnter2D (Collider2D other)
	{
	}

	public void ToIADogResting()
	{
		enemy.currentState = enemy.iaDogResting;
	}

	public void ToIADogFollowing()
	{
	}

	private void Follow ()
	{
		if (enemy.transform.position.x != enemy.target.position.x) {
			if (enemy.transform.position.x > enemy.target.position.x) {
				enemy.transform.Translate (new Vector3 (-enemy.followSpeed * (Time.deltaTime), 0, 0));
				if (enemy.transform.position.x == enemy.target.position.x) {
					enemy.close = false;
					ToIADogResting();
				}
			} else if (enemy.transform.position.x < enemy.target.position.x) {
				enemy.transform.Translate (new Vector3 (enemy.followSpeed * Time.deltaTime, 0, 0)); 
				if (enemy.transform.position.x == enemy.target.position.x) {
					enemy.close = false;
					ToIADogResting();
				}
			}
		}
	}
}
     