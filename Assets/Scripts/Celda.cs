using TMPro;
using UnityEngine;

public class Celda : MonoBehaviour
{
    [SerializeField] private int x, y;
    [SerializeField] private bool bomb;
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private int number;
    [SerializeField] private bool revealed=false;

    private void Start()
    {
        if (!this.bomb)
        {
            this.number = Generator.Instance.getBombsAround(x, y);
        }
    }
    public void setY(int y)
    {
        this.y = y;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public int getY()
    {
        return this.y;
    }

    public int getX()
    {
        return this.x;
    }

    public void setBomb(bool bomb)
    {
        this.bomb = bomb;
    }

    public bool isBomb()
    {
        return bomb;
    }
    public bool isRevealed()
    {
        return revealed;
    }
    public void setRevealed(bool revealed)
    {
        this.revealed = revealed;
    }

    public void setText(string text)
    {
        this.tmpText.text = text;
    }

    public void setNumber(int number)
    {
        this.number = number;
    }

    public int getNumber()
    {
        return this.number;
    }

    private void OnMouseDown()
    {
        ClickCell();
    }
    public void ClickCell()
    {
        if (this.bomb)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;
            StartCoroutine(Generator.Instance.Ending(false));
        }
        else
        {

            if (this.number == 0)
            {
                Generator.Instance.RevealEmptyCells(x, y);
            }
            revealed = true;
            tmpText.text = number.ToString();
            Generator.Instance.addTry();



            if ((Generator.Instance.getWidth() * Generator.Instance.getHeight()) - Generator.Instance.getNBombs() == Generator.Instance.getNTries() && Generator.Instance.isWinner())
            {
                StartCoroutine(Generator.Instance.Ending(true));
            }
        }
    }
    internal void Show()
    {
        if (!this.bomb)
            tmpText.text = Generator.Instance.getBombsAround(x, y).ToString();
        else
            GetComponent<SpriteRenderer>().material.color = Color.red;
    }

}
