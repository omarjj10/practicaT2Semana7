using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vidaController : MonoBehaviour
{
    public Text vidaText;
    public int vida = 3;
    public void desminuirVida(int v)
    {
        vida -= v;
    }
    
    void Update()
    {
        vidaText.text = "Numero de vidas del personaje: " + vida;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Enemy")
        {
            desminuirVida(1);
        }
    }
}
