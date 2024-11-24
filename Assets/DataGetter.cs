using Proyecto26;
using UnityEngine;
using System;
using System.Collections.Generic;

public class DataGetter : MonoBehaviour
{
    private readonly string baseUrl = "https://cartest-1a577-default-rtdb.firebaseio.com/";

    // Método para obtener datos de Firebase basados en el tipo de prueba (ej. "Rampa", "Pull")
    public void GetData(string testKey)
    {
        string url = $"{baseUrl}/game/{testKey}.json";

        RestClient.Get(url, (RequestException exception, ResponseHelper response) =>
        {
            if (exception != null)
            {
                Debug.LogError($"Error fetching data for {testKey}: {exception.Message}");
                return;
            }

            // Aquí imprimimos los datos obtenidos
            Debug.Log($"Data for {testKey}: {response.Text}");

            // Parsear los datos dependiendo de la estructura esperada (puedes ajustar esto según sea necesario)
            ParseData(testKey, response.Text);
        });
    }

    // Método para parsear los datos JSON según la clave del test
    private void ParseData(string testKey, string jsonData)
    {
        var data = JsonUtility.FromJson<Dictionary<string, object>>(jsonData);

        foreach (var entry in data)
        {
            Debug.Log($"Processing {entry.Key} for {testKey}");

            // Parseamos los datos de cada "Data1", "Data2", etc.
            if (testKey == "Rampa")
            {
                RampaData rampaData = JsonUtility.FromJson<RampaData>(entry.Value.ToString());
                Debug.Log($"Rampa Data: Car Mass: {rampaData.carMass}, Car Speed: {rampaData.carSpeed}, Ramp Distance: {rampaData.rampDistance}");
            }
            else if (testKey == "Pull")
            {
                PullData pullData = JsonUtility.FromJson<PullData>(entry.Value.ToString());
                Debug.Log($"Pull Data: Car Mass: {pullData.carMass}, Car Speed: {pullData.carSpeed}, Cube Mass: {pullData.cubeMass}, Joint Force: {pullData.jointForce}");
            }
            else if (testKey == "CubeNetwork")
            {
                CubeNetworkData cubeNetworkData = JsonUtility.FromJson<CubeNetworkData>(entry.Value.ToString());
                Debug.Log($"Cube Network Data: Car Mass: {cubeNetworkData.carMass}, Car Speed: {cubeNetworkData.carSpeed}, Cube Mass: {cubeNetworkData.cubeMass}, Joint Force: {cubeNetworkData.jointForce}");
            }
            else if (testKey == "Push")
            {
                PushData pushData = JsonUtility.FromJson<PushData>(entry.Value.ToString());
                Debug.Log($"Push Data: Car Mass: {pushData.carMass}, Car Speed: {pushData.carSpeed}, Cube Mass: {pushData.cubeMass}, Cube Speed: {pushData.cubeSpeed}");
            }
        }
    }

    [Serializable]
    public class RampaData
    {
        public float carMass;
        public float carSpeed;
        public float rampDistance;
    }

    [Serializable]
    public class PullData
    {
        public float carMass;
        public float carSpeed;
        public float cubeMass;
        public float jointForce;
    }

    [Serializable]
    public class CubeNetworkData
    {
        public float carMass;
        public float carSpeed;
        public float cubeMass;
        public float jointForce;
    }

    [Serializable]
    public class PushData
    {
        public float carMass;
        public float carSpeed;
        public float cubeMass;
        public float cubeSpeed;
    }
}
