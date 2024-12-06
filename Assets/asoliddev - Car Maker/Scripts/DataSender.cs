using Proyecto26;
using System;
using UnityEngine;

public abstract class DataSender : MonoBehaviour
{
    protected readonly string baseUrl = "https://cartest-1a577-default-rtdb.firebaseio.com/game";
    private int dataCounter = 0;

    protected abstract string TestKey { get; }
    protected abstract object CreateTestData();

    private void Start()
    {
        FetchLastDataCounter();
    }

    private void FetchLastDataCounter()
    {
        RestClient.Get($"{baseUrl}/{TestKey}.json", (RequestException exception, ResponseHelper response) =>
        {
            if (exception != null)
            {
                Debug.LogWarning($"No previous data found for {TestKey}. Starting from Data1.");
                dataCounter = 0;
                return;
            }

            foreach (string key in response.Text.Split('\"'))
            {
                if (key.StartsWith("Data"))
                {
                    int number = int.Parse(key.Replace("Data", ""));
                    if (number > dataCounter)
                    {
                        dataCounter = number;
                    }
                }
            }

            Debug.Log($"Last data index for {TestKey}: Data{dataCounter}");
        });
    }

    public void SendData()
    {
        // Generar una clave única basada en la fecha y hora actuales, separadas por un guion medio.
        string uniqueKey = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        object data = CreateTestData();

        RestClient.Put(BuildURL(uniqueKey), data, (exception, response) =>
        {
            if (exception != null)
            {
                Debug.LogError($"Error sending data for {TestKey}: {exception.Message}");
            }
            else
            {
                Debug.Log($"Data for {TestKey} sent successfully: {response.Text}");
            }
        });
    }


    private string BuildURL(string uniqueKey)
    {
        return $"{baseUrl}/{TestKey}/{uniqueKey}.json";
    }
}

