using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject celda;
    [SerializeField] private int width, height;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(celda, new Vector2(i, j), Quaternion.identity);
                
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
