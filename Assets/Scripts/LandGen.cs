using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandGen : MonoBehaviour {
    #region Decla
    public GameObject cube;
    public int interpolLev = 1;
    public int x = 100;
    public int y = 100;
    public int z = 100;
    public int mer = 0;
    public int sable = 10;
    public int herbe = 25;
    public int roche = 33;
    public int neige = 40;

    private grid2D<int> map;
    #endregion
    #region Unity fct
    // Use this for initialization
    void Start() {
        map = new grid2D<int>();
        print("run");
        defineAlti();
        print("alti ok");
        test();
        print("test ok");
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
    #region other fct

    void defineAlti() {
        for (int idx = 0; idx < x; idx++)
        {
            for (int idy = 0; idy < y; idy++)
            {
                int alti = (int)((UnityEngine.Random.value * 4 - 1) * z / 4);
                
                map.Add(idx, idy, alti);
            }
        }
        interpol(interpolLev);
    }

    void interpol(int size = 1)
    {
        int its = 0;
        float moy = 0;

        //int pond = 1;
        grid2D<int> newVals = new grid2D<int>();
        //**
        for(int idx = 0; idx < x; idx++)
        {
            for (int idy = 0; idy < y; idy++)
            {
                //coins
                //x - size ; y - size
                //x - size ; y + size
                //x + size ; y - size
                //x + size ; y + size
                //**
                for(int xx = idx - size; xx <= idx + size; xx++)
                {
                    for (int yy = idy - size; yy <= idy + size; yy++)
                    {
                        
                        if (map.ContainsObject(idx, idy))
                        {
                            its++;
                            moy += map.GetItem(idx, idy);
                        }
                    }
                }
                //**/
                newVals.Add(idx, idy, (int)(moy /= (float)its));
                
                its = 1;
                

            }
        }

        map = newVals;
        //**/
    }



    void test() {
        //Hashtable grid = new Hashtable();
        GameObject inst;
        foreach(String v in map.Keys)
        {
            for(int idz = Mathf.Min((int)map[v], 0); idz < Mathf.Max(1, (int)map[v]); idz++){
                Texture2D txt = (Texture2D)Resources.Load("Textures/Sandy");

                if(idz <= mer)
                {
                    //txt = (Texture2D)Resources.Load("Water Light Blue");
                }
                if (idz > mer)
                {
                    //txt = (Texture2D)Resources.Load("Sandy Orange");
                }
                if (idz > sable)
                {
                    //txt = (Texture2D)Resources.Load("Grass");
                }
                if (idz > herbe)
                {
                    //txt = (Texture2D)Resources.Load("Brown Stony Light");
                }
                if (idz > roche)
                {
                    //txt = (Texture2D)Resources.Load("Brown Stony");
                }
                if (idz > neige)
                {
                    //txt = (Texture2D)Resources.Load("Grey Stone");
                }

                inst = Instantiate(cube, new Vector3(v.x, v.y, idz), Quaternion.identity);
                inst.GetComponent<Renderer>().material.mainTexture = txt;
            }
        }
    }

    #endregion
}


public class grid2D<T>
{
    #region Decla

    protected struct grd2D
    {
        public T Obj { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    protected Dictionary<String, grd2D> used;
    internal IEnumerable<String> Keys;

    

    #endregion

    #region public fct
    public grid2D()
    {
        this.used = new Dictionary<String, T>();
        this.Keys = used.Keys;
    }


    protected String MakeKey(int x, int y)
    {
        return "(" + x + ";" + y + ")";
    }

    public void Add(int x, int y, T obj)
    {
        String key = this.MakeKey(x, y);
        if (used.ContainsKey(key))
        {
            used[key] = new grd2D(){ Obj = obj ; X = x; Y = y };
            return;
        }
        used.Add(key, obj);
    }

    public T GetItem(int x, int y)
    {
        String key = makeKey(x, y);
        if (used.ContainsKey(key))
        {
            return used[key];
        }
        return default(T);
    }

    #endregion

    #region indexable
    public T this[String index]
    {
        get
        {
            return this.used[index];
        }

        set
        {
            this.used[index] = value;
        }
    }
    #endregion

    #region Internal fct

    

    internal bool ContainsObject(int idx, int idy)
    {
        return this.used.ContainsKey(makeKey(idx, idy));
    }

    #endregion
}


public sealed class Singleton
{
    private static Singleton instance = null;
    private static readonly object padlock = new object();
    private static List<Vector2> vect2Instances = new List<Vector2>();

    Singleton()
    {
        vect2Instances = new List<Vector2>();
    }
    
    public Vector2 GetVect(Vector2 vec)
    {
        foreach (Vector2 v in vect2Instances)
        {
            if (vec.Equals(v))
            {
                return v;
            }
        }
        vect2Instances.Add(vec);
        return vec;
    }


    public static Singleton Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}