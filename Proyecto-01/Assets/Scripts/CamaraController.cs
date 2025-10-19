using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform objetivo;
    public float velocidadCamara = 0.025f;
    public Vector3 desplazameinto;

    private void LateUpdate()
    {
        Vector3 posicionDeseada = objetivo.position + desplazameinto;

        Vector3 positionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);
    
        transform.position = positionSuavizada;
    }
}
