using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Image rellenoBarraVida;
    private PlayerController PlayerController;
    private float vidaMaxima;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        vidaMaxima = PlayerController.vidas;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraVida.fillAmount = PlayerController.vidas / vidaMaxima;
    }
}
