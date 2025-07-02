using UnityEngine;
using UnityEngine.SceneManagement;

public class SpiritController : MonoBehaviour
{
    // Este es el método que se llamará cuando la animación termine
    public void OnVictoryAnimationEnd()
    {
        // Cambiar de escena cuando termine la animación
        SceneManager.LoadScene("MainScene");
    }
}