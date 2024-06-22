using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputData : MonoBehaviour
{
    public TextMeshPro output;
    public TMP_InputField quote;
    private List<string> inputList = new List<string>();
    private const int maxInputs = 20;

    //Textinputs cant do them as prefebs cause they are supposed to spawn in different locations
    public TextMeshPro textField1;
    public TextMeshPro textField2;
    public TextMeshPro textField3;
    public TextMeshPro textField4;
    public TextMeshPro textField5;
    public TextMeshPro textField6;
    public TextMeshPro textField7;
    public TextMeshPro textField8;
    public TextMeshPro textField9;
    public TextMeshPro textField10;
    public TextMeshPro textField11;
    public TextMeshPro textField12;
    public TextMeshPro textField13;
    public TextMeshPro textField14;
    public TextMeshPro textField15;
    public TextMeshPro textField16;
    public TextMeshPro textField17;
    public TextMeshPro textField18;
    public TextMeshPro textField19;
    public TextMeshPro textField20;


    public void StoreInput()
    {
        string inputText = quote.text.Trim(); // Trim to remove leading and trailing whitespace

        if (!string.IsNullOrWhiteSpace(inputText))
        {
            if (inputList.Count < maxInputs)
            {
                inputList.Add(inputText);
            }
            else
            {
                // If the list has reached its maximum capacity, remove the oldest input
                // and add the new input at the beginning.
                inputList.RemoveAt(0);
                inputList.Insert(0, inputText);
            }

            int inputSlot = inputList.Count; // Get the slot of the input (index + 1)
            Debug.Log("Input stored in slot " + inputSlot + ": " + inputText); // Log the input along with its slot
        }

        UpdateDisplay();
        quote.text = "";
    }


    private void UpdateDisplay()
    {
        // Fill all text fields with filler text
        for (int i = 0; i < maxInputs; i++)
        {
            if (i < inputList.Count)
            {
                // Assign the input from the list to the corresponding text field based on the index
                switch (i)
                {
                    case 0:
                        textField1.text = inputList[i];
                        break;
                    case 1:
                        textField2.text = inputList[i];
                        break;
                    case 2:
                        textField3.text = inputList[i];
                        break;
                    case 3:
                        textField4.text = inputList[i];
                        break;
                    case 4:
                        textField5.text = inputList[i];
                        break;
                    case 5:
                        textField6.text = inputList[i];
                        break;
                    case 6:
                        textField7.text = inputList[i];
                        break;
                    case 7:
                        textField8.text = inputList[i];
                        break;
                    case 8:
                        textField9.text = inputList[i];
                        break;
                    case 9:
                        textField10.text = inputList[i];
                        break;
                    case 10:
                        textField11.text = inputList[i];
                        break;
                    case 11:
                        textField12.text = inputList[i];
                        break;
                    case 12:
                        textField13.text = inputList[i];
                        break;
                    case 13:
                        textField14.text = inputList[i];
                        break;
                    case 14:
                        textField15.text = inputList[i];
                        break;
                    case 15:
                        textField16.text = inputList[i];
                        break;
                    case 16:
                        textField17.text = inputList[i];
                        break;
                    case 17:
                        textField18.text = inputList[i];
                        break;
                    case 18:
                        textField19.text = inputList[i];
                        break;
                    case 19:
                        textField20.text = inputList[i];
                        break;
                    // Add more cases for additional text fields
                    default:
                        break;
                }
            }
        }
    }

    public void OnSubmitButtonClicked()
    {
        StoreInput();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StoreInput();
        }
    }
}
