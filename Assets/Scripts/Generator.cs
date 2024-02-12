using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject celda;
    [SerializeField] private int width, height;
    public int nBombs;

    private GameObject[][] map;
    private int x, y;


    // Start is called before the first frame update
    void Start()
    {

        //Generamos el mapa interactivo
        map = new GameObject[width][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new GameObject[height];
        }

        //Creamos las celdas
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i][j]=Instantiate(celda, new Vector2(i, j), Quaternion.identity);
                map[i][j].GetComponent<ScriptCelda>().setX(i);
                map[i][j].GetComponent<ScriptCelda>().setY(j);

            }
        }

        //Situamos la cámara
        Camera.main.transform.position= new Vector3((float)width/2 - 0.5f, (float)height/2 - 0.5f, -10); //restamos el tamaño de la celda para centrarlo

        //Situamos las bombas aleatoriamente
        for(int i = 0; i < nBombs; i++)
        {
            x = Random.Range(0, width);
            y = Random.Range(0, height);

            if (!map[x][y].GetComponent<ScriptCelda>().isBomb())
            {
                map[x][y].GetComponent<ScriptCelda>().setBomb(true);
                map[x][y].GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                i--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
