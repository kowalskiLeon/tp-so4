using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pagina{

    public GameObject file;
    public string[] entry;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void inicializar()
    {
        entry = new string[8];
        for (int i = 0; i < 8; i++)
        {
            entry[i] = "";
        }
    }

    

    public void setEntry(int i, string name)
    {
        entry[i] = name;
    }

    public string getEntry(int i)
    {
        return entry[i];
    }

    public int returnPageSize ()
    {
        int size = 0;

        for(int i = 0; i < 8; i++)
        {
            if (entry[i] != "") size++;
        }

        return size;
    }

}
