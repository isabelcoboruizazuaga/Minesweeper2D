using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Celda : MonoBehaviour
{
    [SerializeField] private int x, y;
    [SerializeField] private bool bomb;
    [SerializeField] private TMP_Text tmpText;

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

    public void setText(string text)
    {
        this.tmpText.text = text;
    }

    private void OnMouseDown()
    {
        if (this.bomb)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;
            Generator.Instance.setWinner(false);
        }
        else
        {
            tmpText.text = Generator.Instance.getBombsAround(x, y).ToString();
            Generator.Instance.addTest();
            if ((Generator.Instance.getWidth() * Generator.Instance.getHeight()) - Generator.Instance.getNBombs() == Generator.Instance.getNTest() && Generator.Instance.isWinner())
            {
                Debug.Log("has ganado");
            }
        }
    }

}
