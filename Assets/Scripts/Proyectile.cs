using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5.0f;  // Velocidad base del proyectil
    private Vector3 direction = Vector3.right;  // Direcci�n hacia la derecha
    private bool hasCollided = false;  // Evita que el proyectil se destruya al chocar

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector3 direction, float duration)
    {
        // Calcular la velocidad para que tarde "duration" en llegar al destino
        float actualSpeed = Vector3.Distance(transform.position, transform.position + direction * 10) / duration;
        rb.velocity = direction * actualSpeed;  // Asignar la velocidad calculada
    }

    void Update()
    {
        // Si no quieres que el proyectil sea destruido o reposicionado, quita este c�digo
        // Solo debe moverse mediante el Rigidbody2D y no manualmente en Update
    }

    // M�todo para restablecer el proyectil en una nueva posici�n
    public void ResetProjectile(Vector3 newPosition)
    {
        rb.velocity = Vector3.zero;  // Detenemos el movimiento
        transform.position = newPosition;  // Cambiamos la posici�n del proyectil
        hasCollided = false;  // Restablecemos el estado de colisi�n
    }

    // M�todo para gestionar el comportamiento de la colisi�n
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided)
        {
            // Aqu� puedes agregar l�gica de colisi�n, como la interacci�n con el enemigo o jugador
            hasCollided = true;
            rb.velocity = Vector3.zero;  // Detenemos el proyectil
            // Si es necesario, llama a otra funci�n que maneje el impacto
        }
    }
}