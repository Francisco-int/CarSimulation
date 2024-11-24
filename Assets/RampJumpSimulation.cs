using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampPlacer : MonoBehaviour
{
    public GameObject rampPrefab; // Prefab de la rampa (aunque ya no lo usaremos para instanciar)
    public Transform ramp1; // Transform de la rampa inicial
    public GameObject ramp2; // Referencia a la segunda rampa asignada manualmente en el Inspector
    public float distanceBetweenRamps = 10f; // Distancia configurable entre las rampas
    public GameObject colliderObject; // Objeto que tendr� el BoxCollider

    void Start()
    {
        // Verificar si ya se ha asignado la segunda rampa en el Inspector
        if (ramp2 == null)
        {
            Debug.LogError("Por favor, asigna la segunda rampa en el Inspector.");
            return;
        }

        // Si la segunda rampa est� asignada, colocamos su posici�n.
        PlaceRamp2();

        // Crear y ajustar el BoxCollider
        CreateAndAdjustCollider();
    }

    /// <summary>
    /// Coloca la segunda rampa en la posici�n adecuada sin instanciarla, solo la mueve.
    /// </summary>
    void PlaceRamp2()
    {
        if (ramp1 == null || ramp2 == null)
        {
            Debug.LogError("Por favor, asigna tanto la rampa inicial como la segunda rampa en el Inspector.");
            return;
        }

        // Calcular la nueva posici�n de la segunda rampa usando el eje X
        Vector3 ramp2Position = ramp1.position + new Vector3(distanceBetweenRamps, 0, 0);

        // Mover la segunda rampa a la nueva posici�n calculada
        ramp2.transform.position = ramp2Position;

        // Asegurar que la segunda rampa est� rotada a 180 grados en el eje Y
        ramp2.transform.rotation = ramp1.rotation * Quaternion.Euler(0, 180, 0);

        Debug.Log("Segunda rampa colocada a una distancia de: " + distanceBetweenRamps + " unidades con rotaci�n de 180 grados.");
    }

    /// <summary>
    /// Crea un BoxCollider en el objeto asignado y ajusta su tama�o en funci�n de la distancia entre las rampas.
    /// </summary>
    void CreateAndAdjustCollider()
    {
        if (colliderObject == null)
        {
            Debug.LogError("Por favor, asigna un objeto para el BoxCollider en el Inspector.");
            return;
        }

        // Agregar un BoxCollider al objeto asignado si no tiene uno
        BoxCollider boxCollider = colliderObject.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = colliderObject.AddComponent<BoxCollider>();
        }

        // Ajustar el tama�o del BoxCollider en el eje X seg�n la distancia entre las rampas
        boxCollider.size = new Vector3(distanceBetweenRamps, 1, 3.50f);

        // Colocar el BoxCollider en el centro de las rampas
        Vector3 colliderCenter = ramp1.position + new Vector3(distanceBetweenRamps / 2, 0, 0);
        boxCollider.center = colliderCenter - colliderObject.transform.position;

        Debug.Log("BoxCollider creado y ajustado a la distancia entre rampas.");
    }

    /// <summary>
    /// Actualiza la posici�n de la segunda rampa con una nueva distancia.
    /// Esta funci�n es llamada por el 'RampInfoDisplay' cuando se modifica la distancia.
    /// </summary>
    public void UpdateRampPositions()
    {
        // Actualizar la distancia entre rampas
        distanceBetweenRamps = distanceBetweenRamps;

        if (ramp2 != null)
        {
            // Mover la segunda rampa a la nueva posici�n calculada
            Vector3 newRampPosition = ramp1.position + new Vector3(distanceBetweenRamps, 0, 0);
            ramp2.transform.position = newRampPosition;

            Debug.Log("La posici�n de la segunda rampa se ha actualizado a: " + newRampPosition);
        }
        else
        {
            Debug.LogError("La segunda rampa a�n no ha sido asignada.");
        }

        // Actualizar el BoxCollider
        CreateAndAdjustCollider();
    }

    void OnDrawGizmos()
    {
        if (ramp1 != null)
        {
            Gizmos.color = Color.green;

            // Dibujar l�nea entre la rampa inicial y la posici�n de la segunda rampa
            Vector3 ramp2Position = ramp1.position + new Vector3(distanceBetweenRamps, 0, 0);
            Gizmos.DrawLine(ramp1.position, ramp2Position);

            // Representar la posici�n de la segunda rampa como un cubo
            Gizmos.DrawWireCube(ramp2Position, new Vector3(2, 1, 2));
        }
    }
}
