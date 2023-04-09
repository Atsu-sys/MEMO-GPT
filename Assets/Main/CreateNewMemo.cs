using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class CreateNewMemo : MonoBehaviour
{
    [SerializeField] MemoEditPanel memoEditPanel;
    private Button createMemoButton;
    
    void Start()
    {
        createMemoButton = GetComponent<Button>();
        createMemoButton.onClick.AddListener(OnClickedCreateMemoButton);
    }
    private void OnClickedCreateMemoButton()
    {        
        memoEditPanel.ShowCreateMemo().Forget();
    }
}
