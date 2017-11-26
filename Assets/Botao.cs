using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour {

    public GameObject texto;
    public string txt;
    public int tipo;
    /*
     * 
     * 0 - pasta
     * 1 - arquivo texto
     * 2 - arquivo
     * 
     */

	// Use this for initialization
	void Start () {
        char[] separator = {'/', '\\'};
        texto.GetComponent<TextMesh>().text = txt.Split(separator)[txt.Split(separator).Length -1];

	}
	
    public void clicou(int i)
    {
        if(i == 0)
        {
            GameObject.Find("Gerenciador").GetComponent<Gerenciador>().abrirPasta(gameObject);
        }
        else
        {
            GameObject.Find("Gerenciador").GetComponent<Gerenciador>().abrirArquivo(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
