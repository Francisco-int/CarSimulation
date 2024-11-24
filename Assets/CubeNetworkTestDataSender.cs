using System;
using UnityEngine;
using UnityEngine.UI;

public class CubeNetworkTestDataSender : DataSender, ITestDataSender
{
    public string results; // "Aprobado" o "Desaprobado"
    public CubeNetworkGenerator cubeNetworkScript;
    public CarControler carController;  // Referencia al script CarController
    public Transform scrollViewContent;  // Referencia al Content del ScrollView
    public GameObject textPrefab;        // Prefab para mostrar datos
    public GameObject scrollView;        // Referencia al ScrollView completo

    private bool isScrollViewVisible = false; // Estado del ScrollView
    private string testResult = ""; // Almacena si es "Aprobado" o "Desaprobado"

    protected override string TestKey => "CubeNetwork";
    private void Start()
    {
        scrollView.SetActive(false);
    }
    protected override object CreateTestData()
    {
        // Obtener la masa y velocidad del auto desde el CarController
        float carMass = carController.GetComponent<Rigidbody>().mass;
        float carSpeed = carController.speed;  // Usamos la variable 'speed' del CarController

        // Crear los datos
        var data = new CubeNetworkData
        {
            carMass = carMass,
            carSpeed = carSpeed,
            cubeMass = cubeNetworkScript.cubeMass,
            jointForce = cubeNetworkScript.breakForce,
            result = testResult // Incluye el resultado de la prueba
        };

        // Mostrar datos en el ScrollView
        AppendDataToScrollView(data);

        return data;
    }

    [Serializable]
    public class CubeNetworkData
    {
        public float carMass;
        public float carSpeed;
        public float cubeMass;
        public float jointForce;
        public string result; // Almacena "Aprobado" o "Desaprobado"
    }

    private void AppendDataToScrollView(CubeNetworkData data)
    {
        // Crear el texto que contendrá todos los datos
        string content = $"Car Mass: {data.carMass}\n" +
                         $"Car Speed: {data.carSpeed}\n" +
                         $"Cube Mass: {data.cubeMass}\n" +
                         $"Joint Force: {data.jointForce}\n" +
                         $"Result: {data.result}\n" +
                         $"-------------";

        // Instanciar el prefab y configurarlo como hijo del Content
        GameObject textObject = Instantiate(textPrefab, scrollViewContent);
        Text textComponent = textObject.GetComponent<Text>();
        textComponent.text = content;
    }

    private void ToggleScrollView()
    {
        isScrollViewVisible = !isScrollViewVisible;
        scrollView.SetActive(isScrollViewVisible);
    }
    public void SetTestResult(string result)
    {
        testResult = result;
        SendData();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("TestObject"))
        {
            SetTestResult(results);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))  // Presionar 'T' para mostrar y enviar datos
        {
            SendData();
        }

        if (Input.GetKeyDown(KeyCode.M))  // Presionar 'M' para mostrar/ocultar el ScrollView
        {
            ToggleScrollView();
        }
    }
}
