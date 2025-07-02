using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BattleManager : MonoBehaviour
{
    public Text playerHealthText;  // Barra de vida de Beetlemon
    public Text enemyHealthText;   // Barra de vida del enemigo
    public Text playerText;        // Texto para mostrar el estado del jugador (ganar/perder)
    public Text enemyText;         // Texto para mostrar el estado del enemigo
    public bool resolveattack;
    public int playerHealth = 100; // Vida inicial de Beetlemon
    public int enemyHealth = 100;  // Vida inicial del enemigo

    public GameObject[] spiritsarray;
    public GameObject player;

    public GameObject[] enemiesByMap;  // Array de enemigos por mapa
    public GameObject[] enemiesProyectilesByMap;  // Array de enemigos por mapa
    private GameObject currentEnemy;   // El enemigo actual instanciado
    public Transform enemySpawnPoint;  // Punto de spawn del enemigo
    public int currentMap;            // El mapa actual en el que el jugador está


    public Button energyAttackButton;
    public Button abilityAttackButton;
    public Button crushAttackButton;

    public Animator playerAnimator;  // Controlador de animación de Beetlemon
    public Animator enemyAnimator;   // Controlador de animación del enemigo
     
    public Transform playerTransform;  // Referencia a la posición del jugador
    public Transform projectileSpawnPoint;  // Punto desde donde sale el proyectil
    public Transform enemyProjectileSpawnPoint;
    public GameObject energyProjectilePrefab;
    public GameObject energyMKProjectilePrefab;
    public GameObject energyABProjectilePrefab; // Prefab del proyectil de energía
    public GameObject energyAgunimonProjectilePrefab; // Prefab del proyectil de energía
    public GameObject energyBGProjectilePrefab;
    public GameObject energyAldamonProjectilePrefab;
    public GameObject energyLobomonProjectilePrefab; // Prefab del proyectil de energía
    public GameObject energyKendoProjectilePrefab;
    public GameObject energyBeoProjectilePrefab;
    public GameObject enemyProjectilePrefab;

    public Vector3 originalPosition;   // Posición original del jugador
    public float slowMoveSpeed = 1.5f;      // Velocidad a la que el personaje se mueve

    // Establecemos un valor fijo para el borde de la pantalla (independientemente del dispositivo)
    private float offScreenDistance = 5f;

    Vector3 loserEndPosition;
    Vector3 winnerEndPosition;


    //Digimon Damages
    //Beetlemon
    private int beetlemonEnergyDmg;
    private int beetlemonCrushDmg;
    private int beetlemonAbilityDmg;

    //Metalkabuterimon
    private int mkEnergyDmg;
    private int mkCrushDmg;
    private int mkAbilityDmg;

    //AncientBeetlemon
    private int abEnergyDmg;
    private int abCrushDmg;
    private int abAbilityDmg;

    //Agunimon
    private int agunimonEnergyDmg;
    private int agunimonCrushDmg;
    private int agunimonAbilityDmg;

    //BurningGreymon
    private int bgEnergyDmg;
    private int bgCrushDmg;
    private int bgAbilityDmg;

    //Aldamon
    private int aldamonEnergyDmg;
    private int aldamonCrushDmg;
    private int aldamonAbilityDmg;

    //Agunimon
    private int lobomonEnergyDmg;
    private int lobomonCrushDmg;
    private int lobomonAbilityDmg;

    //BurningGreymon
    private int kendoEnergyDmg;
    private int kendoCrushDmg;
    private int kendoAbilityDmg;

    //Aldamon
    private int beoEnergyDmg;
    private int beoCrushDmg;
    private int beoAbilityDmg;


    private void Awake()
    {
        //Beetlemon
        beetlemonEnergyDmg = Random.Range(15, 25);
        beetlemonCrushDmg = Random.Range(20, 30);
        beetlemonAbilityDmg = Random.Range(10, 20);

        //Metalkabuterimon
        mkEnergyDmg = Random.Range(35, 45);
        mkCrushDmg = Random.Range(20, 30);
        mkAbilityDmg = Random.Range(20, 25);

        //AncientBeetlemon
        abEnergyDmg = Random.Range(55, 75);
        abCrushDmg = Random.Range(40, 50);
        abAbilityDmg = Random.Range(50, 55);

        //Agunimon
        agunimonEnergyDmg = Random.Range(15, 25);
        agunimonCrushDmg = Random.Range(15, 25);
        agunimonAbilityDmg = Random.Range(15, 25);

        //BurningGreymon
        bgEnergyDmg = Random.Range(25, 35);
        bgCrushDmg = Random.Range(25, 35);
        bgAbilityDmg = Random.Range(25, 35);

        //Aldamon
        aldamonEnergyDmg = Random.Range(65, 75);
        aldamonCrushDmg = Random.Range(45, 55);
        aldamonAbilityDmg = Random.Range(45, 50);

        //Lobomon
        lobomonEnergyDmg = Random.Range(20, 25);
        lobomonCrushDmg = Random.Range(15, 25);
        lobomonAbilityDmg = Random.Range(20, 30);

        //Kendo
        kendoEnergyDmg = Random.Range(30, 35);
        kendoCrushDmg = Random.Range(35, 45);
        kendoAbilityDmg = Random.Range(20, 35);

        //Aldamon
        beoEnergyDmg = Random.Range(60, 75);
        beoCrushDmg = Random.Range(45, 55);
        beoAbilityDmg = Random.Range(40, 55);
    }

    void Start()
    {
        // Obtener el mapa actual (esto puede venir de otra clase o del GameManager) primera linea es la definitiva
        currentMap = FindObjectOfType<GameManager>().GetCurrentMap();
        //currentMap = 0;

        // Instanciar el enemigo oculto según el mapa
        currentEnemy = Instantiate(enemiesByMap[currentMap], enemySpawnPoint.position, Quaternion.identity);
        enemyAnimator = currentEnemy.GetComponent<Animator>();
        currentEnemy.SetActive(false);  // Ocultar el enemigo al inicio

        if(GameManager.instance.currentBoss == "Ranamon")
        {
            enemyHealth = 100;

        }else if(GameManager.instance.currentBoss == "Mercurymon")
        {
            enemyHealth = 115;

        }
        else if (GameManager.instance.currentBoss == "Lucemon")
        {
            enemyHealth = 160;

        }

        //Proyectil
        enemyProjectilePrefab = enemiesProyectilesByMap[currentMap];
        
        

        //Instanciar player
        if(GameManager.instance.currentSpirit == "Beetlemon")
        {
            player = Instantiate(spiritsarray[0], Vector3.zero, Quaternion.identity );
            playerHealth = 105;
            playerAnimator = player.GetComponent<Animator>();
        }
        else if(GameManager.instance.currentSpirit == "Metalkabuterimon")
        {
            player = Instantiate(spiritsarray[1], Vector3.zero, Quaternion.identity);
            playerHealth = 120; 
            playerAnimator = player.GetComponent<Animator>();
        }
        else if (GameManager.instance.currentSpirit == "AncientBeetlemon")
        {
            player = Instantiate(spiritsarray[2], Vector3.zero, Quaternion.identity);
            playerHealth = 150;
            playerAnimator = player.GetComponent<Animator>();
        }
        else if (GameManager.instance.currentSpirit == "Agunimon")
        {
            player = Instantiate(spiritsarray[3], Vector3.zero, Quaternion.identity);
            playerHealth = 105;
            playerAnimator = player.GetComponent<Animator>();
        }
        else if (GameManager.instance.currentSpirit == "Burninggreymon")
        {
            player = Instantiate(spiritsarray[4], Vector3.zero, Quaternion.identity);
            playerHealth = 120;
            playerAnimator = player.GetComponent<Animator>();
        }
        else if (GameManager.instance.currentSpirit == "Aldamon")
        {
            player = Instantiate(spiritsarray[5], Vector3.zero, Quaternion.identity);
            playerHealth = 150;
            playerAnimator = player.GetComponent<Animator>();
        }
        else if (GameManager.instance.currentSpirit == "Lobomon")
        {
            player = Instantiate(spiritsarray[6], Vector3.zero, Quaternion.identity);
            playerHealth = 105;
            playerAnimator = player.GetComponent<Animator>();
        }
        else if (GameManager.instance.currentSpirit == "Kendogarurumon")
        {
            player = Instantiate(spiritsarray[7], Vector3.zero, Quaternion.identity);
            playerHealth = 120;
            playerAnimator = player.GetComponent<Animator>();
        }
        else if (GameManager.instance.currentSpirit == "Beowolfmon")
        {
            player = Instantiate(spiritsarray[8], Vector3.zero, Quaternion.identity);
            playerHealth = 150;
            playerAnimator = player.GetComponent<Animator>();
        }


        playerTransform = player.transform;
        projectileSpawnPoint = player.GetComponentInChildren<Transform>();

        // Inicializa la barra de vida en la pantalla
        UpdateHealthText();
        originalPosition = playerTransform.position;  // Guardar la posición original

        // Asigna las funciones de los botones de ataque
        energyAttackButton.onClick.AddListener(() => ExecuteAttack("Energy"));
        abilityAttackButton.onClick.AddListener(() => ExecuteAttack("Ability"));
        crushAttackButton.onClick.AddListener(() => ExecuteAttack("Crush"));



    }

    // Método para ejecutar un ataque
    public void ExecuteAttack(string attackType)
    {
        DisableAttackButtons();  // Deshabilitar los botones al inicio del turno

        // Ejecutar ambos ataques simultáneamente
        StartCoroutine(ExecutePlayerAttack(attackType));
    }

    // Corrutina para gestionar el ataque del jugador
    private IEnumerator ExecutePlayerAttack(string playerAttack)
    {
        if (playerAttack == "Energy")
        {
            yield return StartCoroutine(ExecuteEnergyAttack(playerTransform, playerAnimator, Vector3.right, false));
            yield return new WaitForSeconds(1.5f);
            player.SetActive(false);

        }
        else
        {
            yield return StartCoroutine(MoveOffScreen(playerTransform, playerAnimator, Vector3.right, playerAttack, slowMoveSpeed));
        }

        // Una vez el jugador ha hecho su movimiento, el enemigo realiza su ataque
        string enemyAttack = GetEnemyAttack();
        if (enemyAttack == "Energy")
        {
            currentEnemy.SetActive(true);
            yield return StartCoroutine(ExecuteEnergyAttack(currentEnemy.transform, enemyAnimator, Vector3.left, true));
            yield return new WaitForSeconds(1.5f);
            currentEnemy.SetActive(false);

        }
        else
        {
            currentEnemy.SetActive(true);
            yield return StartCoroutine(MoveOffScreen(currentEnemy.transform, enemyAnimator, Vector3.left, enemyAttack, slowMoveSpeed));
        }

        // Fase de colisión en el centro de la pantalla
        yield return StartCoroutine(PerformCollision(playerAttack, enemyAttack));

        EnableAttackButtons();  // Habilitar los botones de ataque para el próximo turno
    }

    private IEnumerator ExecuteEnergyAttack(Transform attackerTransform, Animator animator, Vector3 direction, bool isEnemy)
    {
        animator.SetTrigger("EnergyAttack");

        // Retroceder ligeramente antes de lanzar el proyectil
        Vector3 initialPosition = attackerTransform.position + direction * -0.5f;
        float moveDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            attackerTransform.position = Vector3.Lerp(attackerTransform.position, initialPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Instanciar el proyectil
        GameObject projectile = null;
        if (isEnemy)
        {
            projectile = Instantiate(enemyProjectilePrefab, enemyProjectileSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            if (GameManager.instance.currentSpirit == "Beetlemon")
            {
                projectile = Instantiate(energyProjectilePrefab, new Vector3(1, 0, 0), Quaternion.identity);
                projectile.SetActive(true);  // Aparecer el proyectil

                // Esperar antes de agrandar el proyectil
                yield return new WaitForSeconds(0.5f);

                // Agrandar el proyectil
                projectile.transform.localScale *= 3.5f;
            }
            else if (GameManager.instance.currentSpirit == "Metalkabuterimon")
            {
                projectile = Instantiate(energyMKProjectilePrefab, new Vector3(1, 0, 0), Quaternion.identity);
                projectile.SetActive(true);  // Aparecer el proyectil

                // Esperar antes de agrandar el proyectil
                yield return new WaitForSeconds(0.5f);

                // Agrandar el proyectil
                projectile.transform.localScale *= 3.5f;
            }
            else if (GameManager.instance.currentSpirit == "AncientBeetlemon")
            {
                projectile = Instantiate(energyABProjectilePrefab, new Vector3(1, 0, 0), Quaternion.identity);
                projectile.SetActive(true);  // Aparecer el proyectil

                // Esperar antes de agrandar el proyectil
                yield return new WaitForSeconds(0.5f);

                // Agrandar el proyectil
                projectile.transform.localScale *= 3.5f;
            }
            else if (GameManager.instance.currentSpirit == "Agunimon")
            {
                projectile = Instantiate(energyAgunimonProjectilePrefab, new Vector3(1, 0, 0), Quaternion.identity);
                projectile.SetActive(true);  // Aparecer el proyectil

                // Esperar antes de agrandar el proyectil
                yield return new WaitForSeconds(0.5f);
            }
            else if (GameManager.instance.currentSpirit == "Burninggreymon")
            {
                projectile = Instantiate(energyBGProjectilePrefab, new Vector3(1, 0, 0), Quaternion.identity);
                projectile.SetActive(true);  // Aparecer el proyectil

                // Esperar antes de agrandar el proyectil
                yield return new WaitForSeconds(0.5f);
            }
            else if (GameManager.instance.currentSpirit == "Aldamon")
            {
                projectile = Instantiate(energyAldamonProjectilePrefab, new Vector3(1, 0, 0), Quaternion.identity);
                projectile.SetActive(true);  // Aparecer el proyectil

                // Esperar antes de agrandar el proyectil
                yield return new WaitForSeconds(0.5f);
            }
            else if (GameManager.instance.currentSpirit == "Lobomon")
            {
                projectile = Instantiate(energyLobomonProjectilePrefab, new Vector3(1, 0, 0), Quaternion.identity);
                projectile.SetActive(true);  // Aparecer el proyectil

                // Esperar antes de agrandar el proyectil
                yield return new WaitForSeconds(0.5f);
            }
            else if (GameManager.instance.currentSpirit == "Kendogarurumon")
            {
                projectile = Instantiate(energyKendoProjectilePrefab, new Vector3(1, 0, 0), Quaternion.identity);
                projectile.SetActive(true);  // Aparecer el proyectil

                // Esperar antes de agrandar el proyectil
                yield return new WaitForSeconds(0.5f);
            }
            else if (GameManager.instance.currentSpirit == "Beowolfmon")
            {
                projectile = Instantiate(energyBeoProjectilePrefab, new Vector3(1, 0, 0), Quaternion.identity);
                projectile.SetActive(true);  // Aparecer el proyectil

                // Esperar antes de agrandar el proyectil
                yield return new WaitForSeconds(0.5f);
            }


        }

        // Esperar antes de lanzarlo
        yield return new WaitForSeconds(0.5f);

        // Lento: 3 segundos para llegar al punto de colisión
        projectile.GetComponent<Projectile>().Launch(direction, 3.0f);

        yield return null;
    }


    // Corrutina para mover un personaje fuera de la pantalla
    private IEnumerator MoveOffScreen(Transform attackerTransform, Animator animator, Vector3 direction, string attackType, float moveSpeed)
    {
        animator.SetTrigger(attackType + "Attack");

        // Mover hasta que el personaje esté fuera de la pantalla lentamente
        Vector3 offScreenPosition = attackerTransform.position + direction * offScreenDistance;
        float elapsedTime = 0f;
        float moveDuration = 100f;  // Duración más larga para movimientos más lentos

        while (elapsedTime < moveDuration)
        {
            if (elapsedTime < 2.0f)
            {
                attackerTransform.position = Vector3.Lerp(attackerTransform.position, offScreenPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            else
            {
                break;
            }
        }

        attackerTransform.gameObject.SetActive(false);  // Desactivar el personaje al salir de la pantalla
    }
    private IEnumerator PerformCollision(string playerAttack, string enemyAttack)
    {
        // Reaparecer ambos personajes desde los extremos
        Vector3 playerStartPosition = new Vector3(-offScreenDistance, playerTransform.position.y, playerTransform.position.z);
        Vector3 enemyStartPosition = new Vector3(offScreenDistance, currentEnemy.transform.position.y, currentEnemy.transform.position.z);
        playerTransform.position = playerStartPosition;
        currentEnemy.transform.position = enemyStartPosition;

        // Puntos de colisión en el centro
        Vector3 collisionPointPlayer = new Vector3(-1, 0, 0);  // Punto de colisión para el jugador
        Vector3 collisionPointEnemy = new Vector3(1, 0, 0);    // Punto de colisión para el enemigo
        float moveDuration = 3f;  // Aumentar la duración para que lleguen más lentamente al punto de colisión
        float elapsedTime = 0f;

        playerTransform.gameObject.SetActive(true);  // Reactivar el jugador
        currentEnemy.SetActive(true);  // Reactivar el enemigo

        GameObject playerProjectile = null;
        GameObject enemyProjectile = null;

        // Si el jugador usa un ataque de proyectil
        if (playerAttack == "Energy")
        {
            
            if (GameManager.instance.currentSpirit == "Beetlemon")
            {
                playerProjectile = Instantiate(energyProjectilePrefab, new Vector3(-offScreenDistance, 0, 0), Quaternion.identity);
                playerProjectile.transform.localScale *= 3.5f;
            }
            else if (GameManager.instance.currentSpirit == "Metalkabuterimon")
            {
                playerProjectile = Instantiate(energyMKProjectilePrefab, new Vector3(-offScreenDistance, 0, 0), Quaternion.identity);
                playerProjectile.transform.localScale *= 3.5f;
            }
            else if (GameManager.instance.currentSpirit == "AncientBeetlemon")
            {
                playerProjectile = Instantiate(energyABProjectilePrefab, new Vector3(-offScreenDistance, 0, 0), Quaternion.identity);
                playerProjectile.transform.localScale *= 3.5f;
            }
            else if (GameManager.instance.currentSpirit == "Agunimon")
            {
                playerProjectile = Instantiate(energyAgunimonProjectilePrefab, new Vector3(-offScreenDistance, 0, 0), Quaternion.identity);
            }
            else if (GameManager.instance.currentSpirit == "Burninggreymon")
            {
                playerProjectile = Instantiate(energyBGProjectilePrefab, new Vector3(-offScreenDistance, 0, 0), Quaternion.identity);
            }
            else if (GameManager.instance.currentSpirit == "Aldamon")
            {
                playerProjectile = Instantiate(energyAldamonProjectilePrefab, new Vector3(-offScreenDistance, 0, 0), Quaternion.identity);
            }
            else if (GameManager.instance.currentSpirit == "Lobomon")
            {
                playerProjectile = Instantiate(energyLobomonProjectilePrefab, new Vector3(-offScreenDistance, 0, 0), Quaternion.identity);
            }
            else if (GameManager.instance.currentSpirit == "Kendogarurumon")
            {
                playerProjectile = Instantiate(energyKendoProjectilePrefab, new Vector3(-offScreenDistance, 0, 0), Quaternion.identity);
            }
            else if (GameManager.instance.currentSpirit == "Beowolfmon")
            {
                playerProjectile = Instantiate(energyBeoProjectilePrefab, new Vector3(-offScreenDistance, 0, 0), Quaternion.identity);
            }
            // Aumentar el tamaño del proyectil
            playerProjectile.SetActive(true);
            playerTransform.gameObject.SetActive(false);  // Ocultar al jugador durante el ataque de proyectil
        }
        else
        {
            // Mantener la animación del jugador en el ataque seleccionado
            playerAnimator.SetTrigger(playerAttack + "Attack");
        }

        // Si el enemigo usa un ataque de proyectil
        if (enemyAttack == "Energy")
        {
            enemyProjectile = Instantiate(enemyProjectilePrefab, new Vector3(offScreenDistance, 0, 0), Quaternion.identity);
            enemyProjectile.transform.localScale *= 3.5f;  // Aumentar el tamaño del proyectil del enemigo
            currentEnemy.SetActive(false);  // Ocultar al enemigo durante el ataque de proyectil
        }
        else
        {
            enemyAnimator.SetTrigger(enemyAttack + "Attack");
        }

        // Mover proyectiles o personajes hacia el centro lentamente
        while (elapsedTime < moveDuration)
        {
            if (playerProjectile != null)
            {
                playerProjectile.transform.position = Vector3.Lerp(new Vector3(-offScreenDistance, 0, 0), collisionPointPlayer, elapsedTime / moveDuration);
            }
            else
            {
                playerTransform.position = Vector3.Lerp(playerStartPosition, collisionPointPlayer, elapsedTime / moveDuration);
            }

            if (enemyProjectile != null)
            {
                enemyProjectile.transform.position = Vector3.Lerp(new Vector3(offScreenDistance, 0, 0), collisionPointEnemy, elapsedTime / moveDuration);
            }
            else
            {
                currentEnemy.transform.position = Vector3.Lerp(enemyStartPosition, collisionPointEnemy, elapsedTime / moveDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Resolver el choque de ataques
        ResolveAttack(playerAttack, enemyAttack);

        // Empujar al perdedor después del ataque
        
        if (GameManager.instance.choosenKid.Contains("jp"))
        {
            if (playerAttack == "Energy" && enemyAttack == "Crush" || playerAttack == "Crush" && enemyAttack == "Ability" || playerAttack == "Ability" && enemyAttack == "Energy")
            {
                loserEndPosition = currentEnemy.transform.position + Vector3.right * offScreenDistance;
                winnerEndPosition = playerProjectile != null ? playerProjectile.transform.position + Vector3.right * 5f : playerTransform.position + Vector3.right * 5f;
            }
            else
            {

                loserEndPosition = playerTransform.position + Vector3.left * offScreenDistance;
                winnerEndPosition = enemyProjectile != null ? enemyProjectile.transform.position + Vector3.left * 5f : currentEnemy.transform.position + Vector3.left * 5f;
            }
        }

        if (GameManager.instance.choosenKid.Contains("takuya"))
        {
            if (playerAttack == "Energy" && enemyAttack == "Ability" || playerAttack == "Crush" && enemyAttack == "Energy" || playerAttack == "Ability" && enemyAttack == "Crush")
            {
                loserEndPosition = currentEnemy.transform.position + Vector3.right * offScreenDistance;
                winnerEndPosition = playerProjectile != null ? playerProjectile.transform.position + Vector3.right * 5f : playerTransform.position + Vector3.right * 5f;
            }
            else
            {

                loserEndPosition = playerTransform.position + Vector3.left * offScreenDistance;
                winnerEndPosition = enemyProjectile != null ? enemyProjectile.transform.position + Vector3.left * 5f : currentEnemy.transform.position + Vector3.left * 5f;
            }
        }

        if (GameManager.instance.choosenKid.Contains("koji"))
        {
            if (playerAttack == "Energy" && enemyAttack == "Energy" || playerAttack == "Crush" && enemyAttack == "Crush" || playerAttack == "Ability" && enemyAttack == "Ability")
            {
                loserEndPosition = currentEnemy.transform.position + Vector3.right * offScreenDistance;
                winnerEndPosition = playerProjectile != null ? playerProjectile.transform.position + Vector3.right * 5f : playerTransform.position + Vector3.right * 5f;
            }
            else
            {

                loserEndPosition = playerTransform.position + Vector3.left * offScreenDistance;
                winnerEndPosition = enemyProjectile != null ? enemyProjectile.transform.position + Vector3.left * 5f : currentEnemy.transform.position + Vector3.left * 5f;
            }
        }

        elapsedTime = 0f;
        moveDuration = 2f;  // Reducir la velocidad del empuje
        while (elapsedTime < moveDuration)
        {

            if(GameManager.instance.choosenKid.Contains("jp")){
                //Player gana
                if (playerAttack == "Energy" && enemyAttack == "Crush" || playerAttack == "Crush" && enemyAttack == "Ability" || playerAttack == "Ability" && enemyAttack == "Energy")
                {
                    currentEnemy.transform.position = Vector3.Lerp(collisionPointEnemy, loserEndPosition, elapsedTime / moveDuration);
                    if (playerProjectile != null)
                    {
                        playerProjectile.transform.position = Vector3.Lerp(collisionPointPlayer, winnerEndPosition, elapsedTime / moveDuration);
                    }
                    else
                    {
                        playerTransform.position = Vector3.Lerp(collisionPointPlayer, winnerEndPosition, elapsedTime / moveDuration);
                    }
                }
                else  //El enemigo retrocede
                {
                    playerTransform.position = Vector3.Lerp(collisionPointPlayer, loserEndPosition, elapsedTime / moveDuration);
                    if (enemyProjectile != null)
                    {
                        enemyProjectile.transform.position = Vector3.Lerp(collisionPointEnemy, winnerEndPosition, elapsedTime / moveDuration);
                    }
                    else
                    {
                        currentEnemy.transform.position = Vector3.Lerp(collisionPointEnemy, winnerEndPosition, elapsedTime / moveDuration);
                    }
                }
            }

            if (GameManager.instance.choosenKid.Contains("takuya")){
                //Player gana
                if (playerAttack == "Energy" && enemyAttack == "Ability" || playerAttack == "Crush" && enemyAttack == "Energy" || playerAttack == "Ability" && enemyAttack == "Crush")
                {
                    currentEnemy.transform.position = Vector3.Lerp(collisionPointEnemy, loserEndPosition, elapsedTime / moveDuration);
                    if (playerProjectile != null)
                    {
                        playerProjectile.transform.position = Vector3.Lerp(collisionPointPlayer, winnerEndPosition, elapsedTime / moveDuration);
                    }
                    else
                    {
                        playerTransform.position = Vector3.Lerp(collisionPointPlayer, winnerEndPosition, elapsedTime / moveDuration);
                    }
                }
                else  //El enemigo retrocede
                {
                    playerTransform.position = Vector3.Lerp(collisionPointPlayer, loserEndPosition, elapsedTime / moveDuration);
                    if (enemyProjectile != null)
                    {
                        enemyProjectile.transform.position = Vector3.Lerp(collisionPointEnemy, winnerEndPosition, elapsedTime / moveDuration);
                    }
                    else
                    {
                        currentEnemy.transform.position = Vector3.Lerp(collisionPointEnemy, winnerEndPosition, elapsedTime / moveDuration);
                    }
                }
            }

            if (GameManager.instance.choosenKid.Contains("koji"))
            {
                //Player gana
                if (playerAttack == "Energy" && enemyAttack == "Energy" || playerAttack == "Crush" && enemyAttack == "Crush" || playerAttack == "Ability" && enemyAttack == "Ability")
                {
                    currentEnemy.transform.position = Vector3.Lerp(collisionPointEnemy, loserEndPosition, elapsedTime / moveDuration);
                    if (playerProjectile != null)
                    {
                        playerProjectile.transform.position = Vector3.Lerp(collisionPointPlayer, winnerEndPosition, elapsedTime / moveDuration);
                    }
                    else
                    {
                        playerTransform.position = Vector3.Lerp(collisionPointPlayer, winnerEndPosition, elapsedTime / moveDuration);
                    }
                }
                else  //El enemigo retrocede
                {
                    playerTransform.position = Vector3.Lerp(collisionPointPlayer, loserEndPosition, elapsedTime / moveDuration);
                    if (enemyProjectile != null)
                    {
                        enemyProjectile.transform.position = Vector3.Lerp(collisionPointEnemy, winnerEndPosition, elapsedTime / moveDuration);
                    }
                    else
                    {
                        currentEnemy.transform.position = Vector3.Lerp(collisionPointEnemy, winnerEndPosition, elapsedTime / moveDuration);
                    }
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // No destruir proyectiles después del empuje, simplemente moverlos fuera de la pantalla
        if (playerProjectile != null)
        {
            playerProjectile.transform.position = new Vector3(offScreenDistance, playerProjectile.transform.position.y, playerProjectile.transform.position.z);
        }
        if (enemyProjectile != null)
        {
            enemyProjectile.transform.position = new Vector3(-offScreenDistance, enemyProjectile.transform.position.y, enemyProjectile.transform.position.z);
        }

        playerAnimator.SetTrigger("Idle");
        enemyAnimator.SetTrigger("Idle");

        // Restablecer posiciones
        playerTransform.position = Vector3.zero;
        currentEnemy.transform.position = Vector3.zero;
        playerTransform.gameObject.SetActive(true);
        currentEnemy.SetActive(false);
    }

    // Método para obtener un ataque aleatorio del enemigo
    private string GetEnemyAttack()
    {
        string[] attackTypes = { "Energy", "Ability", "Crush" };
        int randomIndex = Random.Range(0, attackTypes.Length);
        return attackTypes[randomIndex];
    }

    // Método para resolver el choque de ataques
    private void ResolveAttack(string playerAttack, string enemyAttack)
    {
        if(GameManager.instance.currentSpirit == "Beetlemon"){
            if (playerAttack == "Energy" && enemyAttack == "Crush")
            {
                enemyHealth -= beetlemonEnergyDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Crush" && enemyAttack == "Ability")
            {
                enemyHealth -= beetlemonCrushDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Ability" && enemyAttack == "Energy")
            {
                enemyHealth -= beetlemonAbilityDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else
            {
                playerHealth -= Random.Range(10, 20);  // El enemigo golpea al jugador
                resolveattack = true;
            }
        }

        if (GameManager.instance.currentSpirit == "Metalkabuterimon")
        {
            if (playerAttack == "Energy" && enemyAttack == "Crush")
            {
                enemyHealth -= mkEnergyDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Crush" && enemyAttack == "Ability")
            {
                enemyHealth -= mkCrushDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Ability" && enemyAttack == "Energy")
            {
                enemyHealth -= mkAbilityDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else
            {
                playerHealth -= Random.Range(10, 20);  // El enemigo golpea al jugador
                resolveattack = true;
            }
        }

        if (GameManager.instance.currentSpirit == "AncientBeetlemon")
        {
            if (playerAttack == "Energy" && enemyAttack == "Crush")
            {
                enemyHealth -= abEnergyDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Crush" && enemyAttack == "Ability")
            {
                enemyHealth -= abCrushDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Ability" && enemyAttack == "Energy")
            {
                enemyHealth -= abAbilityDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else
            {
                playerHealth -= Random.Range(10, 20);  // El enemigo golpea al jugador
                resolveattack = true;
            }
        }

        if (GameManager.instance.currentSpirit == "Agunimon")
        {
            if (playerAttack == "Crush" && enemyAttack == "Energy")
            {
                enemyHealth -= agunimonEnergyDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Ability" && enemyAttack == "Crush")
            {
                enemyHealth -= agunimonCrushDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Energy" && enemyAttack == "Ability")
            {
                enemyHealth -= agunimonAbilityDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else
            {
                playerHealth -= Random.Range(10, 20);  // El enemigo golpea al jugador
                resolveattack = true;
            }
        }

        if (GameManager.instance.currentSpirit == "Burninggreymon")
        {
            if (playerAttack == "Crush" && enemyAttack == "Energy")
            {
                enemyHealth -= agunimonEnergyDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Ability" && enemyAttack == "Crush")
            {
                enemyHealth -= agunimonCrushDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Energy" && enemyAttack == "Ability")
            {
                enemyHealth -= agunimonAbilityDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else
            {
                playerHealth -= Random.Range(10, 20);  // El enemigo golpea al jugador
                resolveattack = true;
            }
        }

        if (GameManager.instance.currentSpirit == "Aldamon")
        {
            if (playerAttack == "Crush" && enemyAttack == "Energy")
            {
                enemyHealth -= agunimonEnergyDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Ability" && enemyAttack == "Crush")
            {
                enemyHealth -= agunimonCrushDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Energy" && enemyAttack == "Ability")
            {
                enemyHealth -= agunimonAbilityDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else
            {
                playerHealth -= Random.Range(10, 20);  // El enemigo golpea al jugador
                resolveattack = true;
            }
        }

        if (GameManager.instance.currentSpirit == "Lobomon")
        {
            if (playerAttack == "Crush" && enemyAttack == "Crush")
            {
                enemyHealth -= lobomonEnergyDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Ability" && enemyAttack == "Ability")
            {
                enemyHealth -= lobomonCrushDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Energy" && enemyAttack == "Energy")
            {
                enemyHealth -= lobomonAbilityDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else
            {
                playerHealth -= Random.Range(10, 20);  // El enemigo golpea al jugador
                resolveattack = true;
            }
        }

        if (GameManager.instance.currentSpirit == "Kendogarurumon")
        {
            if (playerAttack == "Crush" && enemyAttack == "Crush")
            {
                enemyHealth -= kendoEnergyDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Ability" && enemyAttack == "Ability")
            {
                enemyHealth -= kendoCrushDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Energy" && enemyAttack == "Energy")
            {
                enemyHealth -= kendoAbilityDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else
            {
                playerHealth -= Random.Range(10, 20);  // El enemigo golpea al jugador
                resolveattack = true;
            }
        }

        if (GameManager.instance.currentSpirit == "Beowolfmon")
        {
            if (playerAttack == "Crush" && enemyAttack == "Crush")
            {
                enemyHealth -= beoEnergyDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Ability" && enemyAttack == "Ability")
            {
                enemyHealth -= beoCrushDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else if (playerAttack == "Energy" && enemyAttack == "energy")
            {
                enemyHealth -= beoAbilityDmg;  // El jugador golpea al enemigo
                resolveattack = true;
            }
            else
            {
                playerHealth -= Random.Range(10, 20);  // El enemigo golpea al jugador
                resolveattack = true;
            }
        }

        UpdateHealthText();

        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            UpdateHealthText();
            EndBattle(true);  // El jugador ganó
        }
        else if (playerHealth <= 0)
        {
            EndBattle(false);  // El jugador perdió
        }
    }

    // Método para deshabilitar los botones de ataque
    private void DisableAttackButtons()
    {
        energyAttackButton.interactable = false;
        abilityAttackButton.interactable = false;
        crushAttackButton.interactable = false;
    }

    // Método para habilitar los botones de ataque
    private void EnableAttackButtons()
    {
        energyAttackButton.interactable = true;
        abilityAttackButton.interactable = true;
        crushAttackButton.interactable = true;
    }

    // Método para actualizar las barras de vida en pantalla
    private void UpdateHealthText()
    {
        playerHealthText.text = playerHealth.ToString();
        enemyHealthText.text = enemyHealth.ToString();
    }

    // Método para terminar la batalla
    private void EndBattle(bool playerWon)
    {
        // Desactivar los botones de ataque después de la batalla
        DisableAttackButtons();

        if (playerWon)
        {
            playerText.text = "WIN";
            enemyText.text = "ENEMY DEFEATED";
            // Activar la animación de victoria
            
            playerAnimator.SetTrigger("Victory");
            DisableAttackButtons();
            // Reducir el tamaño del jugador antes de la animación de victoria

            
                StartCoroutine(ShrinkPlayerBeforeVictory());
            
            
        }
        else
        {
            playerText.text = "LOSE";
            enemyText.text = "";
            SceneManager.LoadScene("MainScene");
        }

        DisableAttackButtons();
    }
    // Corrutina para reducir el tamaño del jugador y luego reproducir la animación de victoria
    private IEnumerator ShrinkPlayerBeforeVictory()
    {
        yield return new WaitForSeconds(0.5f);
        // Reducir el tamaño del jugador
        if (GameManager.instance.choosenKid.Contains("jp"))
        {
            Vector3 originalScale = playerTransform.localScale;
            Vector3 targetScale = originalScale * 0.3f;  // Reducir a la mitad

            float shrinkDuration = 0.3f;
            float elapsedTime = 0f;

            // Proceso para hacer más pequeño al player de manera suave
            while (elapsedTime < shrinkDuration)
            {
                playerTransform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / shrinkDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        DisableAttackButtons();
        // Activar la animación de victoria

        if (GameManager.instance.currentBoss == "Ranamon")
        {
            if (GameManager.instance.choosenKid.Contains("jp"))
            {
                GameManager.instance.AddTextToList("Metalkabuterimon");

            }
            if (GameManager.instance.choosenKid.Contains("takuya"))
            {
                Debug.Log("Spirit conseguido");

                GameManager.instance.AddTextToList("Burninggreymon");
            }
            if (GameManager.instance.choosenKid.Contains("koji"))
            {
                Debug.Log("Spirit conseguido");

                GameManager.instance.AddTextToList("Kendogarurumon");
            }
        }
        
        if(GameManager.instance.currentBoss == "Mercurymon")
        {
            if (GameManager.instance.choosenKid.Contains("jp"))
            {
                GameManager.instance.AddTextToList("AncientBeetlemon");

            }
            if (GameManager.instance.choosenKid.Contains("takuya"))
            {
                GameManager.instance.AddTextToList("Aldamon");
            }
            if (GameManager.instance.choosenKid.Contains("koji"))
            {
                GameManager.instance.AddTextToList("Beowolfmon");
            }
        }

        playerAnimator.SetTrigger("Victory");

        if(GameManager.instance.currentBoss == "Lucemon")
        {
            SceneManager.LoadScene("CharacterSelectionScene");
        }

        // La animación manejará el cambio de escena usando el evento
        yield return null;
        DisableAttackButtons();
    }

    private IEnumerator OnVictoryAnimationEnd()
    {

        SceneManager.LoadScene("MainScene");
        yield return null;
    }
}



