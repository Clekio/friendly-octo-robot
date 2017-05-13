using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour {

	bool waterfall;
	bool bubble;
	bool wind;
	bool swirl;

    bool SpawnOnDrawPosition;

    [HideInInspector]
    public string magicName;
    [HideInInspector]
    public Vector2 position;

	bool waterGrowing;
	bool bubbleGrowing;

	public GameObject waterBall;
	public GameObject waterBubble;
	public GameObject viento1;
	public GameObject remol1;

	//public Camera camera;

	public Texture2D cursorTextureVie;
	public Texture2D cursorTextureRaf;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	public float maxWaterSize = 0.9f;
	public float waterGrowSpeed = 0.01f;

	public float maxBubbleSize = 0.9f;
	public float bubbleGrowSpeed = 0.01f;

	GameObject bola;

	float waterGravity;
	Transform waterTransform;

	float bubbleGravity;
	Transform bubbleTransform;

	private Gestures scr_gestos;

	// Use this for initialization
	void Start () {
		
		ResetOption ();
		//if (!camera) {
		//	camera = Camera.main;
		//}

		waterGrowing = false;
		bubbleGrowing = false;

		waterGravity = waterBall.GetComponent<Rigidbody2D> ().gravityScale;
		bubbleGravity = waterBubble.GetComponent<Rigidbody2D> ().gravityScale;

        scr_gestos = GetComponent<Gestures>();

	}
	
	// Update is called once per frame
	void Update () {

        //Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 21));
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));

        if (Input.GetMouseButtonDown(1))
        {
            if (magicName == "waterfall")
            {
                bola = Instantiate(waterBall, new Vector3(p.x, p.y, 0), Quaternion.identity);
                waterTransform = bola.GetComponent<Transform>();
                bola.GetComponent<Rigidbody2D>().gravityScale = 0;
                waterGrowing = true;
			}
			else if (magicName == "bubble")
			{
				bola = Instantiate(waterBubble, new Vector3(p.x, p.y, 0), Quaternion.identity);
				bubbleTransform = bola.GetComponent<Transform>();
				bola.GetComponent<Rigidbody2D>().gravityScale = 0;
				bubbleGrowing = true;
			}

            else if (magicName == "wind")
            {
                Cursor.SetCursor(cursorTextureVie, hotSpot, cursorMode);
                Instantiate(viento1, new Vector3(p.x, p.y, 0), Quaternion.identity);
                Cursor.SetCursor(null, Vector2.zero, cursorMode);
                ResetOption();
            }
            else if (magicName == "swirl")
            {
                Cursor.SetCursor(cursorTextureRaf, hotSpot, cursorMode);
                Instantiate(remol1, new Vector3(p.x, p.y, 0), Quaternion.identity);
                Cursor.SetCursor(null, Vector2.zero, cursorMode);
                ResetOption();
            }
            else if (magicName == "thunder")
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(p.x, p.y), Vector2.zero);

                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<scr_thunderEffects>().Accion();
                }
                magicName = null;
            }
        }

        else if (Input.GetMouseButtonUp(1))
        {
            if (magicName == "waterfall")
            {
                waterGrowing = false;
                bola.GetComponent<Rigidbody2D>().gravityScale = waterGravity;
                ResetOption();
            }
			else if (magicName == "bubble")
			{
				bubbleGrowing = false;
				bola.GetComponent<Rigidbody2D>().gravityScale = bubbleGravity;
				ResetOption();
			}
        }

        

        if (Input.GetKeyDown ("1")) {
			ActivateWaterfall ();
		}
		else if (Input.GetKeyDown ("2")) {
			ActivateBubble ();
		}
        else if (Input.GetKeyDown ("3")) {
			ActivateWind ();
		}
        else if (Input.GetKeyDown ("4")) {
			ActivateSwirl ();
		}
		else if (Input.GetKeyDown ("5")) {
			ActivateThunder ();
		}

		if (waterGrowing) {
			if (waterTransform.localScale.x < maxWaterSize) {
				waterTransform.localScale += new Vector3 (waterGrowSpeed, waterGrowSpeed, 0);
			} else {
				waterTransform.localScale = new Vector3 (maxWaterSize, maxWaterSize, 1);
			}
		}

		if (bubbleGrowing) {
			if (bubbleTransform.localScale.x < maxBubbleSize) {
				bubbleTransform.localScale += new Vector3 (bubbleGrowSpeed, bubbleGrowSpeed, 0);
			} else {
				bubbleTransform.localScale = new Vector3 (maxBubbleSize, maxBubbleSize, 1);
			}
		}

	}

    public void SpawnMagic(string name, Vector2 position)
    {
        Debug.Log(position);
        //if (name == "waterfall")
        //{
        //    bola = Instantiate(waterBall, new Vector3(position.x, position.y, 0), Quaternion.identity);
        //    waterTransform = bola.GetComponent<Transform>();
        //    bola.GetComponent<Rigidbody2D>().gravityScale = 0;
        //    waterGrowing = true;
        //}
        //else if (magicName == "bubble")
        //{
        //    bola = Instantiate(waterBubble, new Vector3(p.x, p.y, 0), Quaternion.identity);
        //    bubbleTransform = bola.GetComponent<Transform>();
        //    bola.GetComponent<Rigidbody2D>().gravityScale = 0;
        //    bubbleGrowing = true;
        //}

        if (name == "wind")
        {
            Cursor.SetCursor(cursorTextureVie, hotSpot, cursorMode);
            Instantiate(viento1, new Vector3(position.x, position.y, 0), Quaternion.identity);
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            ResetOption();
        }
        else if (name == "swirl")
        {
            Cursor.SetCursor(cursorTextureRaf, hotSpot, cursorMode);
            Instantiate(remol1, new Vector3(position.x, position.y, 0), Quaternion.identity);
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            ResetOption();
        }
        else if (name == "thunder")
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(position.x, position.y), Vector2.zero);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<scr_thunderEffects>().Accion();
            }
            name = null;
        }
    }

	void ResetOption (){
        magicName = null;
	}

	void ActivateWaterfall (){
        magicName = "waterfall";
    }

	void ActivateBubble (){
		magicName = "bubble";
	}

	void ActivateWind (){
        magicName = "wind";
    }

	void ActivateSwirl (){
        magicName = "swirl";
    }

	void ActivateThunder (){
		magicName = "thunder";
	}
}
