using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;


public class MemoItem : MonoBehaviour
{
    public TMP_Text memoText;
    public GameObject editPanel;
    public TMP_InputField editInputField;
    [SerializeField] private Button button;
    private MemoEditPanel _memoEditPanel;
    private int memoIndex;

    public void Initialize(int index,string memo,MemoEditPanel memoEditPanel,TMP_InputField inputField)
    {
        memoIndex = index;
        memoText.text = memo;
        _memoEditPanel = memoEditPanel;
        editPanel = memoEditPanel.gameObject;
        editInputField = inputField;
    }

    private void Start()
    {
        button.onClick.AddListener(OnEditButtonClicked);
    }

    private void OnEditButtonClicked()
    {
        _memoEditPanel.ShowEditMemo(memoIndex, memoText.text).Forget();
    }
}
