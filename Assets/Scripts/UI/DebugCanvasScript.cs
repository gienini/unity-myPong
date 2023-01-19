using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCanvasScript : MonoBehaviour
{
    
    private Rigidbody2D bodyPelota;
    [SerializeField]
    private TextMeshProUGUI velocidadXText = null;
    [SerializeField]
    private TextMeshProUGUI velocidadYText = null;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (bodyPelota == null && FindObjectOfType<PelotaScript>() != null)
        {
            bodyPelota = FindObjectOfType<PelotaScript>().gameObject.GetComponent<Rigidbody2D>();
        }
        if (bodyPelota != null)
        {
            velocidadXText.text = "Velocidad X: " + bodyPelota.velocity.x;
            velocidadYText.text = "Velocidad Y: " + bodyPelota.velocity.y;
        }
    }
}
