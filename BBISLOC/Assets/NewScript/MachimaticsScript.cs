using UnityEngine;
using TMPro;

public class MachimaticsScript : MonoBehaviour
{
    private void Start()
    {
        Initalize();
    }
    void Initalize()
    {
        int sign = UnityEngine.Random.Range(0, 1);
        string operatorSign = string.Empty;
        switch (sign)
        {
            case 0: //Addition Question
                operatorSign = "+";
                num1 = UnityEngine.Random.Range(0, 9);
                int clampValue = 9 - num1;
                num2 = UnityEngine.Random.Range(0, clampValue);
                solution = num1 + num2;
                break;
            case 1: //Subtraction Question
                operatorSign = "-";
                num1 = UnityEngine.Random.Range(0, 9);
                num2 = UnityEngine.Random.Range(0, num1);
                solution = num1 - num2;
                break;
        }
        questionText.text = num1 + operatorSign + num2 + "=";
    }
    public void CheckAnswer(int value)
    {
        if (value == solution)
        {
            PlayerPrefs.SetFloat("ShopPoints", PlayerPrefs.GetFloat("ShopPoints") + 50f);
            questionText.text += value;
            rewardedNotebook.SetActive(true);
        }
    }
    public TextMeshPro questionText;
    int num1, num2, solution;
    public GameObject rewardedNotebook;
}