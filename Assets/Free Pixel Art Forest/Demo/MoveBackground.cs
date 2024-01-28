using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {



	public float speed;
	private float x;
	public float PontoDeDestino;
	public float PontoOriginal;

    private bool isMoving;
    private int direction;

    public static MoveBackground instance;


    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		//PontoOriginal = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

        if (!isMoving)
        {
            return;
        }
        x = transform.position.x;
        x += speed * Time.deltaTime;
        transform.position = new Vector3(x * direction, transform.position.y, transform.position.z);

        Debug.Log("bg is moving");

        if (x <= PontoDeDestino)
        {

            Debug.Log("hhhh");
            x = PontoOriginal;
            transform.position = new Vector3(x * direction, transform.position.y, transform.position.z);
        }


    }

	public void Move(int dir, bool move)
	{
        direction = dir;
        isMoving = move;
    }
}
