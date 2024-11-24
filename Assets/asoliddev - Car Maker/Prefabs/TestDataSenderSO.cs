using UnityEngine;

[CreateAssetMenu(fileName = "TestDataSender", menuName = "Test Data Sender", order = 1)]
public class TestDataSenderSO : ScriptableObject
{
    public MonoBehaviour dataSender;  // Referencia al MonoBehaviour que implementa ITestDataSender
}
