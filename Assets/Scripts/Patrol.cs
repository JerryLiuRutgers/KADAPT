using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {

    private bool movRight = true;
    public float speed = 2;
	
	// Update is called once per frame
	void Update () {
        if (movRight)
        {
            transform.Translate(new Vector3(0, 0, speed));
        }
        else
        {
            transform.Translate(new Vector3(0, 0, -speed));
        }

        if (transform.position.z >= -17)
        {
            movRight = false;
        }

        if (transform.position.z <= -33)
        {
            movRight = true;
        }
	
	}
}
