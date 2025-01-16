using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color permanentColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Image image;
    [SerializeField] private TMP_InputField inputField;

    private int _inputValue;
    
    public int Value;
    public int Row;
    public int Column;
    public event Action OnCellChanged;
    
    public void InitializeCell(int row, int col, int value)
    {
        Row = row;
        Column = col;
        Value = value;
        if (value != 0)
        {
            image.color = permanentColor;
            inputField.text = value.ToString();
            inputField.interactable = false;
        }
        else
        {
            image.color = defaultColor;
            inputField.interactable = true;
            inputField.text = "";
            inputField.onSelect.AddListener(SelectCell);
            inputField.onDeselect.AddListener(DeselectCell);
            inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            inputField.onEndEdit.AddListener(OnSubmitInput);
        }
    }

    public void ResetCell()
    {
        image.color = defaultColor;
        inputField.interactable = true;
        inputField.text = "";
        inputField.onSelect.RemoveListener(SelectCell);
        inputField.onDeselect.RemoveListener(DeselectCell);
        inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
        inputField.onEndEdit.RemoveListener(OnSubmitInput);
    }

    private void SelectCell(string value)
    {
        image.color = selectedColor;
    }

    private void DeselectCell(string value)
    {
        image.color = defaultColor;
    }

    private void OnInputFieldValueChanged(string value)
    {
        if (value.Length != 0)
        {
            inputField.text = value[^1].ToString();
            if (!string.IsNullOrEmpty(inputField.text))
            {
                Value = int.Parse(inputField.text);
            }
            OnCellChanged?.Invoke();
        }
    }

    private void OnSubmitInput(string value)
    {
        image.color = defaultColor;
        inputField.text = value;
        if (!string.IsNullOrEmpty(inputField.text))
        {
            Value = int.Parse(inputField.text);
        }
        OnCellChanged?.Invoke();
    }
}
