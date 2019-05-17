using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicadorSalud : MonoBehaviour
{
    private Text Vida;
    private int vidaRestante;

    public void Actualizar(int vidaPerdida)
    {
        VidaRestante -= vidaPerdida;
        if (VidaRestante < 0) VidaRestante = 0;
        Vida.text = VidaRestante.ToString() + " PV";
    }

    public int VidaRestante
    {
        get => vidaRestante;
        set => vidaRestante = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vida = GetComponent<Text>();
        VidaRestante = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
