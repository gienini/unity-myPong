using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCanvasScript : MonoBehaviour
{
    
    private Rigidbody2D bodyPelota;
    [SerializeField]
    private TextMeshProUGUI velocidadXText;
    [SerializeField]
    private TextMeshProUGUI velocidadYText;

    private void Start()
    {
        bodyPelota = FindObjectOfType<PelotaScript>().gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        velocidadXText.text = "Velocidad X: " + bodyPelota.velocity.x;
        velocidadYText.text = "Velocidad Y: " + bodyPelota.velocity.y;
    }
}
