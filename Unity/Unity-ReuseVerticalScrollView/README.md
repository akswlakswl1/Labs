## 🙌 ReuseVerticalScrollView   
#### This involves using a minimal prefab to dynamically change data and display it in a scroll view.
##### I created it with the help of frames and performance improvement in mind, and I want to share it.😀
## 🛠 How to use
### Step1️⃣
1. Create a scroll view in the Hierarchy 
2. Create a script to attach to the scroll view component, inheriting from ReuseVerticalScrollview
3. Create a class for the item to be used and apply it to the generic &lt;T&gt;

사진추가 사진추가
### Step2️⃣
1. Load data and store it in a list where you want
2. When data is set, call <font color=red>**initdata**</font> to apply or initialize the data.

```csharp
void Init()
{
    //TempDataSet
    List<SampleData> sampleList = new List<SampleData>();
    int len = 5000;//Random.Range(3, 10);
    for (int i = 0; i < len; i++)
    {
        SampleData clubItemData = new SampleData();
        clubItemData.index = i.ToString();
        clubItemData.name = GenerateRandomString(Random.Range(3, 10));
        clubItemData.color = new Color(Random.value, Random.value, Random.value);
        sampleList.Add(clubItemData);
    }


    //When data is set, call InitData to apply or initialize the data.
    InitData(sampleList);
}
```
### Step3️⃣
1. Create an item to be used in the scroll view within the Hierarchy.
2. Create a script and attach it to the item component.
3. Open the script, inherit from ReuseVerticalScrollviewItem, specify the class used in the scroll view for &lt;T&gt;, and implement the Init and UpdateContent methods.

like this
```csharp
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
```
### Step4️⃣
1. Set the <font color="red">**pivot**</font> of the created scroll view item to <font color="red">**X=0, Y=1**</font>, and Make a prefab.
2. In the inspector of the scroll view, assign the created prefab. If you want to add spacing, set the spacing value.

//사진추가
(./Images/마지막실행.png)
