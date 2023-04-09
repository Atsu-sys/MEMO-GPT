using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

public class MemoEditPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField editInputField;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private MemoApp memoApp;
    private int selectedItemIndex;

    private void Start()
    {
        //saveButton.onClick.AddListener(OnSaveButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
        deleteButton.onClick.AddListener(OnDeleteButtonClicked);
    }

    public async UniTask ShowEditMemo(int index, string text)
    {
        selectedItemIndex = index;
        editInputField.text = text;
        deleteButton.gameObject.SetActive(true);
        gameObject.SetActive(true);

        var createButtonEvent = saveButton.onClick.GetAsyncEventHandler(CancellationToken.None);
        var cancelButtonEvent = cancelButton.onClick.GetAsyncEventHandler(CancellationToken.None);
        var deleteButtonEvent = deleteButton.onClick.GetAsyncEventHandler(CancellationToken.None);
        var result = await UniTask.WhenAny(createButtonEvent.OnInvokeAsync(), cancelButtonEvent.OnInvokeAsync(), deleteButtonEvent.OnInvokeAsync());
        if (result == 0)
        {
            string memoText = editInputField.text;
            memoApp.UpdateMemo(selectedItemIndex, memoText);
        }
        else
        {

        }
        HideEditPanel();

    }

    public async UniTask ShowCreateMemo()
    {
        deleteButton.gameObject.SetActive(true);
        gameObject.SetActive(true);
        
        var createButtonEvent = saveButton.onClick.GetAsyncEventHandler(CancellationToken.None);
        var cancelButtonEvent = cancelButton.onClick.GetAsyncEventHandler(CancellationToken.None);
        var deleteButtonEvent = deleteButton.onClick.GetAsyncEventHandler(CancellationToken.None);
        var result = await UniTask.WhenAny(createButtonEvent.OnInvokeAsync(), cancelButtonEvent.OnInvokeAsync(), deleteButtonEvent.OnInvokeAsync());
        if (result == 0)
        {            
            string memoText = editInputField.text;
            memoApp.AddMemo(memoText);
        }
        else
        {
            
        }
        HideEditPanel();
    }    

    private void OnCancelButtonClicked()
    {
        HideEditPanel();
    }

    private void OnDeleteButtonClicked()
    {
        memoApp.RemoveMemo(selectedItemIndex);
        HideEditPanel();
    }
    private void HideEditPanel()
    {
        editInputField.text = "";
        gameObject.SetActive(false);
    }
}
