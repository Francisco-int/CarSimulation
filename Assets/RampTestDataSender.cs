using System;
using UnityEngine;
using UnityEngine.UI;

public class RampTestDataSender : DataSender, ITestDataSender
{
    public string results; // "Aprobado" o "Desaprobado"
    public RampPlacer rampaScript;
    public CarControler carController;
    public DataGetter dataGetter; // Referencia al script DataGetter
    public Transform scrollViewContent;  // Referencia al Content del ScrollView
    public GameObject textPrefab;        // Prefab para mostrar datos
    public GameObject scrollView;        // Referencia al ScrollView completo

    private bool isScrollViewVisible = false; // Estado del ScrollView
    private string testResult = ""; // Almacena si es "Aprobado" o "Desaprobado"

    protected override string TestKey => "Rampa";
    private void Start()
    {
        scrollView.SetActive(false);
    }
    protected override object CreateTestData()
    {
        // Obtener la masa y velocidad del auto desde el CarController
        float carMass = carController.GetComponent<Rigidbody>().mass;
        float carSpeed = carController.speed;

        // Crear los datos y mostrarlos en el ScrollView
        var data = new RampaData
        {
            carMass = carMass,
            carSpeed = carSpeed,
            rampDistance = rampaScript.distanceBetweenRamps,
            result = testResult // Incluye el resultado de la prueba
        };

        AppendDataToScrollView(data);
        return data;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Presionar 'T' para enviar datos
        {
            SendData();
        }

        if (Input.GetKeyDown(KeyCode.M)) // Presionar 'M' para mostrar/ocultar el ScrollView
        {
            ToggleScrollView();
        }

        if (Input.GetKeyDown(KeyCode.G)) // Presionar 'G' para obtener datos de Firebase
        {
            dataGetter.GetData(TestKey); // Obtener datos desde Firebase con la clave del test
        }
    }

    [Serializable]
    public class RampaData
    {
        public float carMass;
        public float carSpeed;
        public float rampDistance;
        public string result; // Almacena "Aprobado" o "Desaprobado"
    }

    private void AppendDataToScrollView(RampaData data)
    {
        // Crear el texto que contendrá todos los datos
        string content = $"Car Mass: {data.carMass}\n" +
                         $"Car Speed: {data.carSpeed}\n" +
                         $"Ramp Distance: {data.rampDistance}\n" +
                         $"Result: {data.result}\n" +
                         $"-------------";

        // Instanciar el prefab y configurarlo como hijo del Content
        GameObject textObject = Instantiate(textPrefab, scrollViewContent);
        Text textComponent = textObject.GetComponent<Text>();
        textComponent.text = content;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("TestObject"))
        {
            SetTestResult(results);
        }
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
}
