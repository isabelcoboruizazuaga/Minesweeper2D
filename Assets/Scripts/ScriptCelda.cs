using TMPro;
using UnityEngine;

public class ScriptCelda : MonoBehaviour
{
    [SerializeField] private int x, y;
    [SerializeField] private bool bomb;
    [SerializeField] private TMP_Text tmpText;

    public int getX() { return x; }
    public int getY() { return y; }
    public bool isBomb() { return bomb; }

    public void setBomb(bool bomb) { this.bomb = bomb; }
    public void setX(int x) { this.x = x; }
    public void setY(int y) { this.y = y; }

    public void setText(string text)
    {
        this.tmpText.text = text;
    }

}
