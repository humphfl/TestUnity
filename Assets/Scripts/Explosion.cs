using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    #region Decla
    public GameObject cube;
    public int size = 30;
    public float density;
    #endregion
    #region Unity fct
    // Use this for initialization
    void Start () {
        test();
	}

    // Update is called once per frame
    void Update () {
		
	}
    #endregion
    #region other fct

    void test() {
        //Hashtable grid = new Hashtable();
        for(int idx = 0; idx < size; idx++)
        {
            for (int idy = 0; idy < size; idy++)
            {
                for (int idz = 0; idz < size; idz++)
                {
                    if (Random.value >= density)
                    {
                        Instantiate(cube, new Vector3(idx, idy, idz), Quaternion.identity);
                        

                    }
                }
            }
        }
    }

    #endregion
}