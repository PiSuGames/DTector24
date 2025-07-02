using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Animator animator;
    public MenuManager menuManager;
    public bool caminando;
    private Coroutine moveCoroutine;  // Almacena la referencia de la corrutina
    public float stepInterval = 0.2f; // Intervalo de incremento de pasos

    public bool isMoving;  // Controla si el jugador se está moviendo
    private float lastStepTime = 0.1f; // Controla el tiempo entre los incrementos de pasos

    void Start()
    {
        animator = GetComponent<Animator>();  // Obtiene el Animator
        menuManager = FindObjectOfType<MenuManager>();  // Encuentra el MenuManager
    }

    void Update()
    {


        Vector3 movement = Vector3.zero;

#if UNITY_EDITOR
        // Movimiento con teclas en el editor
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector3(moveX, moveY, 0);

        // Control de animación al pulsar la barra espaciadora
        if (Input.GetKey(KeyCode.A)) 
        {
           
            
            animator.SetBool("isWalking", true);
            isMoving = true;
            if (!caminando)
            {
                Debug.Log("walk");
                StartWalking();
            }
        }
        else if (movement == Vector3.zero)
        {
            animator.SetBool("isWalking", false);
            isMoving = false;
            if (caminando)
            {
                StopWalking();
            }
        }

#elif UNITY_ANDROID || UNITY_IOS
        // Movimiento con acelerómetro en móviles
        Vector3 accelerometer = Input.acceleration;
        movement = new Vector3(accelerometer.x, accelerometer.y, 0);

        if (movement.magnitude > 0.75f)  // Detectar movimiento brusco
        {
            animator.SetBool("isWalking", true);
            isMoving = true;
            if (!caminando)
            {
                StartWalking();
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            isMoving = false;
            if (caminando)
            {
                StopWalking();
            }
        }
#endif

        // Mover el personaje (en el caso de que haya movimiento)
        if (isMoving)
        {
            Debug.Log("Se Mueve");
            //transform.Translate(movement * speed * Time.deltaTime);

            // Incrementar los pasos cada "stepInterval" segundos
            if (Time.time - lastStepTime >= stepInterval)
            {
                menuManager.AddSteps(1);
                lastStepTime = Time.time;
            }
        }
    }

    // Método que se llama cuando el personaje empieza a caminar
    private void StartWalking()
    {
        caminando = true;
        lastStepTime = Time.time;  // Iniciar el contador de tiempo de los pasos
    }

    // Método que se llama cuando el personaje deja de caminar
    private void StopWalking()
    {
        caminando = false;
        isMoving = false;
    }

    // Método que se llamará desde el evento de la animación
    public void OnBattleAnimationEnd()
    {
        // Llamar al método en MenuManager para cargar la escena de batalla
        menuManager.OnBattleAnimationEnd();
    }
}
