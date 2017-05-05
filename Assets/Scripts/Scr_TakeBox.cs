//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Scr_TakeBox : MonoBehaviour {
//
//	public float altura;
//	public float distance2Take;
//	public GameObject player;
//	public bool GizmoEnabled;
//
//	float distanceRight;
//	float distanceLeft;
//	Scr_Player playerScr;
//	float newHeight;
//
//	void Start () {
//
//		playerScr = player.GetComponent<Scr_Player>();
//		newHeight = transform.position.y + altura;
//
//	}
//	
//	
//	void Update () {
//
//		distanceRight = transform.position.x + distance2Take;
//		distanceLeft = transform.position.x - distance2Take;
//		
//		if (playerScr.takeBox == true && player.transform.position.x > distanceLeft && player.transform.position.x < distanceRight)
//		{
//			transform.position = new Vector3 (player.transform.position.x + 2, newHeight);
//			//transform.position = new Vector3 (player.transform.position.x, newHeight);
//		}
//
//	}
//
//	void OnDrawGizmos()
//    {
//        if (GizmoEnabled)
//        {
//            Gizmos.color = new Color(1, 0, 0, 0.5F);
//            Gizmos.DrawCube (transform.position, new Vector2(distance2Take * 2, 3));
//        }
//    }
//
//}
