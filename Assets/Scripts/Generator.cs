using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Generator : MonoBehaviour
{
    public static Generator Instance;

    [SerializeField] private GameObject celda;
    [SerializeField] private int width, height;
    [SerializeField] private int nBombs, nTest;
    [SerializeField] private Canvas canvas;
    [SerializeField] private bool winner = true;

    [SerializeField] private Canvas canvasFin;
    [SerializeField] private TextMeshProUGUI textoVictoria;

    private GameObject[][] map;

    private int x, y;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        canvasFin.gameObject.SetActive(false);
    }

    public void EasyMap()
    {
        width = 8;
        height = 8;
        nBombs = 10;

        canvas.gameObject.SetActive(false);
        generateMap();
    }

    public void MediumMap()
    {
        width = 16;
        height = 16;
        nBombs = 40;

        canvas.gameObject.SetActive(false);
        generateMap();
    }

    public void HardMap()
    {
        width = 30;
        height = 16;
        nBombs = 99;

        canvas.gameObject.SetActive(false);
        generateMap();
    }

    public void generateMap()
    {
        //Generamos el mapa interno
        map = new GameObject[width][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new GameObject[height];
        }

        //Generamos la pantalla de juego
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i][j] = Instantiate(celda, new Vector3(i, j, 0), Quaternion.identity);
                map[i][j].GetComponent<Celda>().setX(i);
                map[i][j].GetComponent<Celda>().setY(j);
            }
        }

        //Situamos la cámara en el centro
        Camera.main.transform.position = new Vector3(((float)width / 2) - 0.5f, ((float)height / 2) - 0.5f, -10);

        //Situamos las bombas aleatoriamente
        for (int i = 0; i < nBombs; i++)
        {
            x = Random.Range(0, width);
            y = Random.Range(0, height);
            if (!map[x][y].GetComponent<Celda>().isBomb())
            {
                map[x][y].GetComponent<Celda>().setBomb(true);
            }
            else
            {
                //le restamos menos a la i para que haga una iteración más
                i--;
            }
        }
    }

    public int getBombsAround(int x, int y)
    {
        int contador = 0;

        //la primera comprobacion es para que no se salga de los limites del array y pete
        //casilla superior izquierda
        if (x > 0 && y < height - 1 && map[x - 1][y + 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla superior, o sea, si existen casillas por encima
        if (y < height - 1 && map[x][y + 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla superior derecha, compruebo que no estoy en el limite derecho
        if (x < width - 1 && y < height - 1 && map[x + 1][y + 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla izquierda
        if (x > 0 && map[x - 1][y].GetComponent<Celda>().isBomb())
            contador++;

        //casilla derecha
        if (x < width - 1 && map[x + 1][y].GetComponent<Celda>().isBomb())
            contador++;

        //Inferior izquieda
        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla inferior, o sea, si existen casillas por debajo
        if (y > 0 && map[x][y - 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla inferior derecha, compruebo que no estoy en el limite derecho
        if (x < width - 1 && y > 0 && map[x + 1][y - 1].GetComponent<Celda>().isBomb())
            contador++;

        return contador;
    }



    public void RevealEmptyCells(int x, int y)
    {
        int row = x;
        int col = y;

        Queue<Celda> queue = new Queue<Celda>();
        queue.Enqueue(map[row][col].GetComponent<Celda>());

        while (queue.Count > 0)
        {
            Celda currentCell = queue.Dequeue();
            currentCell.setRevealed(true);

            // Comprueba celdas vecinas
            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    if (r >= 0 && r < width && c >= 0 && c < height)
                    {
                        Celda neighbor = map[r][c].GetComponent<Celda>();
                        if (!neighbor.isRevealed())
                        {
                            neighbor.ClickCell();
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }
        }
    }


    public IEnumerator Ending(bool victory)
    {
        setWinner(victory);

        yield return new WaitForSeconds(.5f);

        //Se muestra todo el tablero
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i][j].GetComponent<Celda>().Show();
            }
        }


        yield return new WaitForSeconds(1);
        if (!victory) textoVictoria.text = "Perdiste";
        else textoVictoria.text = "Ganaste!";
        canvasFin.gameObject.SetActive(true);

    }


    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }
    internal void addTry()
    {
        nTest++;
    }

    internal int getWidth()
    {
        return width;
    }

    internal int getHeight()
    {
        return height;
    }

    internal int getNBombs()
    {
        return nBombs;
    }

    internal int getNTries()
    {
        return nTest;
    }

    internal void setWinner(bool v)
    {
        winner = v;
    }

    internal bool isWinner()
    {
        return winner;
    }
}
