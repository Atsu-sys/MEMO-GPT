using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.Threading;
using Cysharp.Threading.Tasks;

public class MemoApp : MonoBehaviour
{
    public MemoEditPanel memoEditPanel;
    public TMP_InputField inputField;
    [SerializeField] private RectTransform content;
    //[SerializeField] private Button createMemoButton;
    [SerializeField] private GameObject memoItemPrefab;

    private List<string> memos = new List<string>();
    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "memos.json");
        LoadMemos();
        DisplayMemos();
        //createMemoButton.onClick.AddListener(CreateMemo);
    }

    public void AddMemo(string memotext)
    {
        if (string.IsNullOrWhiteSpace(memotext)) return;

        memos.Add(memotext);
        SaveMemos();
        DisplayMemos();        
    }

    public void UpdateMemo(int index, string memoText)
    {
        memos[index] = memoText;
        SaveMemos();
        DisplayMemos();
    }

    public void RemoveMemo(int index)
    {
        memos.RemoveAt(index);
        SaveMemos();
        DisplayMemos();
    }

    //private void CreateMemo()
    //{
    //    memoEditPanel.ShowCreateMemo().Forget();        
    //}    

    private void SaveMemos()
    {
        string json = JsonConvert.SerializeObject(memos);
        File.WriteAllText(saveFilePath, json);
    }

    private void LoadMemos()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            memos = JsonConvert.DeserializeObject<List<string>>(json);
        }
    }
    private void DisplayMemos()
    {
        // Clear existing memo items
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // Instantiate new memo items        
        for (int i = 0; i < memos.Count; i++)
        {
            GameObject memoPrefab = Instantiate(memoItemPrefab, content);
            memoPrefab.GetComponent<MemoItem>().Initialize(i, memos[i], memoEditPanel, inputField);
        }
    }
}

