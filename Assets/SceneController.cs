using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Método para cargar una escena específica por su nombre
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Método para salir del juego (opcional, útil para el menú principal)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego..."); // Esto solo se verá en el editor de Unity.
    }
}

