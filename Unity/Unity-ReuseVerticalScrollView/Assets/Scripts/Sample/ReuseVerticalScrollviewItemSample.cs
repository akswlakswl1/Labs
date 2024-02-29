using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReuseVerticalScrollviewItemSample : ReuseVerticalScrollviewItem<SampleData>
{
    [SerializeField] Image imgBG;
    [SerializeField] Text txtIndex;
    [SerializeField] Text txtName;
    [SerializeField] Button btnItem;

    SampleData data;
    public override void Init()
    {
        // If you want to register a button or perform any initialization,
        // you can implement it here.
        btnItem.onClick.AddListener(() =>
        {
            Debug.Log($"<color=cyan>index : {data.index} name : {data.name}</color>");
        });
    }

    public override void UpdateContent(SampleData itemData)
    {
        // Modify the elements you want to change.
        imgBG.color = itemData.color;
        txtIndex.text = itemData.index.ToString();
        txtName.text = itemData.name.ToString();
        data = itemData;
    }
}
