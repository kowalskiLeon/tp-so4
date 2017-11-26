using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Gerenciador : MonoBehaviour {

    public GameObject diretorio;
    public string path;
    public GameObject filePrefab;
    public GameObject folderPrefab;
    public GameObject opPrefab;
    public GameObject areaEdicao;
    public GameObject newName;
    public GameObject areaEdicao2;
    public GameObject selecao;
    public string[] files;
    public string[] folders;
    public int maxFiles;
    public int currentPage;
    public Pagina[] p;
    public int size;
    public GameObject ptext;

    // Use this for initialization
    void Start () {
        inicilizar();
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * 00 - incializar
     * 
     */

     public void inicilizar()
    {
        path = Directory.GetCurrentDirectory();
        diretorio.GetComponent<TextMesh>().text = path;
        getAllFiles();
     
    }
    

    /*
     * 01 - divide string
     * 
     * caso o nome do diretorio seja muito grande
     * 
     */

    public string dividir(string p)
    {
        string s = "";

        for(int i = 0; i < p.Length/3; i++)
        {
            s += p[i];
        }

        s += " ... ";

        for (int i = (4*p.Length/5); i < p.Length; i++)
        {
            s += p[i];
        }

        return s;
    }
    

    /*
     * 10 - obter todos os arquivos em pasta
     * 
     */

    public void getAllFiles()
    {
        files = Directory.GetFiles(path);
        folders = Directory.GetDirectories(path);
        atualizar();
    }

    /*
     * 11 - abrir menu de opções
     * 
     */

    public void abrirMenu()
    {
        files = Directory.GetFiles(path);
    }

    /*
     * 12 - atualiza as opções do menu com os arquivos presentes da pasta
     * 
     */

    public void atualizar()
    {
        Debug.Log("Atualizou");
        size = 1;
        Debug.Log("Qauntidade de arquivos: " + files.Length);
        Debug.Log("Qauntidade de pastas: " + folders.Length);
        int quantidade = files.Length + folders.Length;
        Debug.Log("Qauntidade total: " + quantidade);
        if (quantidade > 8)
        {
            size = (quantidade / 8);
            if (quantidade % 8 != 0) size += 1; 
        }

        p = new Pagina[size];

        for (int i = 0; i < size; i++) {
            Debug.Log("Adicionando nova página");
            p[i] = new Pagina();
            p[i].inicializar();
        }

        int j = 0, c = 0;
        Debug.Log("Qauntidade de páginas: " + size);
        Debug.Log("Pagina atual: " + c);
        for (int i = 0; i < folders.Length; i++)
        {
            p[c].setEntry(j, folders[i]);
            Debug.Log("Adicionando a pasta : " + folders[i] + " a página " + c + " na posição " + j);
            j++;
            if (j == 8)
            {
                j = 0;
                c++;
                Debug.Log("Pagina atual: " + c);
            }

        }

        for (int i = 0; i < files.Length; i++)
        {
            p[c].setEntry(j, files[i]);
            Debug.Log("Adicionando o arquivo : " + files[i] + " a página " + c + " na posição " + j);
            j++;
            if (j == 8)
            {
                j = 0;
                c++;
                Debug.Log("Pagina atual: " + c);
            }

        }
        currentPage = 0;

        ptext.GetComponent<TextMesh>().text = currentPage + 1 + "/" + size;
        instanciar();
    }

    /*
     * 13 - instancia os arquivos de uma pagina
     * 
     */

    public void instanciar()
    {

        //apagar todos os arquivos antigos
        GameObject[] a = GameObject.FindGameObjectsWithTag("File");
        if (a.Length > 0)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Destroy(a[i]);
                Debug.Log("Apagando = " + a[i].GetComponent<Botao>().txt);
            }
        }

        float x = -1.8f;
        float y = 1.8f;
        GameObject aux = filePrefab;
        Debug.Log("Instanciou");
        Debug.Log("Tamanho da Página atual = " + p[currentPage].returnPageSize());
        for (int i = 0; i < p[currentPage].returnPageSize(); i++)
        {
            
            Debug.Log("For - Instance : " + i + " Instanciando " + p[currentPage].getEntry(i));
            if(currentPage*8 + i < folders.Length)aux = folderPrefab;
            else aux = filePrefab;
            aux.GetComponent<Botao>().txt = p[currentPage].getEntry(i);
            Instantiate(aux, new Vector3(x, y, 0), transform.rotation);
            y -= 0.64f;
        }

    }

    /*
     * 14 - mudar de página
     * 
     */

    public void mudarPag(int i)
        {
        currentPage += i;
        if (currentPage < 0) currentPage = size - 1;
        if (currentPage >= size) currentPage = 0;

        ptext.GetComponent<TextMesh>().text = currentPage + 1 + "/" + size;
        instanciar();
        }

    /*
     * 
     * 20 - abrir pasta
     * 
     */

    public void abrirPasta(GameObject b)
    {

        GameObject[] aux = GameObject.FindGameObjectsWithTag("Info");

        if (aux.Length > 0)
        {
            for (int i = 0; i < aux.Length; i++)
            {
                Destroy(aux[i]);
            }
        }

        path = b.GetComponentInParent<Botao>().txt;
        diretorio.GetComponent<TextMesh>().text = path;
        getAllFiles();
    }

    /*
     * 
     * 21 - abrir arquivo
     * 
     */

    public void abrirArquivo(GameObject b)
    {
        GameObject[] aux = GameObject.FindGameObjectsWithTag("Info");

        if(aux.Length > 0)
        {
            for(int i = 0; i < aux.Length; i++)
            {
                Destroy(aux[i]);
            }
        }

        string npath = b.GetComponentInParent<Botao>().txt;
        Instantiate(opPrefab).GetComponent<Opcao>().setarValores(npath);

    }

    /*
     * 
     * 30 - editar Arquivo
     * 
     */

    public void ediArq(string arq)
    {
        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.Translate(18f, 0, 0);

        StreamReader leitor = new StreamReader(arq);
        string txt = "";
        string linha = leitor.ReadLine();
        while (linha != null)
        {
            txt += linha + System.Environment.NewLine;
            linha = leitor.ReadLine();
        }

        areaEdicao.GetComponent<InputField>().text = txt;
        leitor.Close();
    }

    /*
     * 
     * 31 - salvar Arquivo
     * 
     */

    public void saveArq()
    {
        string arq = GameObject.Find("Opções(Clone)").GetComponent<Opcao>().path;

        StreamWriter writer = new StreamWriter(arq);

        writer.Write(areaEdicao.GetComponent<InputField>().text);

        writer.Close();

        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.Translate(-18f, 0, 0);


    }


    /*
     * 
     * 32 - cancelar edição do arquivo
     * 
     */

    public void cancelarEdicao()
    {

        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.Translate(-18f, 0, 0);


    }


    /*
     * 
     * 40 - editar novo Arquivo
     * 
     */

    public void ediNovoArq()
    {
        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.Translate(36f, 0, 0);

    }

    /*
     * 
     * 41 - salvar novo Arquivo
     * 
     */

    public void saveNovoArq()
    {
        string arq = "";
        if (newName.GetComponent<InputField>().text.Length > 0) arq += newName.GetComponent<InputField>().text;
        else arq += "SemNome";

        if (selecao.GetComponent<Toggle>().isOn) arq += ".temp";
        else arq += ".txt";

        StreamWriter writer = new StreamWriter(arq);

        writer.Write(areaEdicao2.GetComponent<InputField>().text);

        writer.Close();

        newName.GetComponent<InputField>().text = "";
        areaEdicao2.GetComponent<InputField>().text = "";
        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.Translate(-36f, 0, 0);
        getAllFiles();

    }


    /*
     * 
     * 42 - cancelar edição do novo arquivo
     * 
     */

    public void cancelarNovoEdicao()
    {
        newName.GetComponent<InputField>().text = "";
        areaEdicao2.GetComponent<InputField>().text = "";
        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.Translate(-36f, 0, 0);
        
    }



    /*
     * f0 - subir diretorio
     * 
     */

    public void upDir()
    {
        path = Directory.GetParent(path).ToString();
        string auxPath = path;
        if (path.Length >= 100)
        {
            auxPath = dividir(path);
        }
        diretorio.GetComponent<TextMesh>().text = auxPath;
        getAllFiles();
    }

    /*
     * f1 - quando fechar o aplicativo
     * apaga arquivos temporarios
     */

    private void OnApplicationQuit()
    {
        for(int i = 0; i < files.Length; i++)
        {
            if (files[i].Contains(".temp"))
            {
                FileInfo f = new FileInfo(files[i]);
                f.Delete();
            }
        }


    }

    /*
     * ff - Sair
     * 
     * Finaliza o programa;
     * 
     */

    public void sair()
    {
        Application.Quit();
    }
}
