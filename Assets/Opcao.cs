using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Opcao : MonoBehaviour {

    public string path;
    public GameObject name;
    public GameObject size;
    public GameObject created;
    public GameObject edited;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setarValores(string p)
    {
        path = p;
        FileInfo f = new FileInfo(path);

        char[] separator = { '/', '\\' };
        name.GetComponent<TextMesh>().text = path.Split(separator)[path.Split(separator).Length - 1];
        size.GetComponent<TextMesh>().text = retornarValorConvertido(f.Length) + "B";
        created.GetComponent<TextMesh>().text = "Criado em: " + f.CreationTime;
        edited.GetComponent<TextMesh>().text = "Editado em: " + f.LastWriteTime;

    }

    public void editar()
    {
        GameObject.Find("Gerenciador").GetComponent<Gerenciador>().ediArq(path);

    }

    public void remover()
    {
        FileInfo f = new FileInfo(path);
        f.Delete();
        GameObject.Find("Gerenciador").GetComponent<Gerenciador>().getAllFiles();
        Destroy(gameObject);

    }

    string retornarValorConvertido(float s)
    {
        int i = 0;
        string v = "";
        while(s > 1024)
        {
            s = s / 1024;
            s = Mathf.Round(s);
            i++;
        }

        v += s;
        switch (i)
        {
            case 1: v += "K"; break;
            case 2: v += "M"; break;
            case 3: v += "G"; break;
            case 4: v += "T"; break;
            case 5: v += "P"; break;
            case 6: v += "WTF"; break;
        }

        return v;
    }

}
