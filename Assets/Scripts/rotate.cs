using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    // Use this for initialization
    #region Decla
    public float x = 0;
    public float y = 0;
    public float z = 0;
    public float speed = 10;

    #endregion

    #region Unity fct

    void Start () {
        x = (Random.value * 2 - 1) * speed;
        y = (Random.value * 2 - 1) * speed;
        z = (Random.value * 2 - 1) * speed;

    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
	}

    #endregion
    #region other fct



    #endregion
    
}
