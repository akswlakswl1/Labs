using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SampleData
{
    public string index;
    public string name;
    public Color color;
}

public class ReuseVerticalScrollviewSample : ReuseVerticalScrollview<SampleData>
{
    private void Start()
    {
        Init();
    }

    void Init()
    {
        //TempData
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




    #region Temp
    private const string PASSWORD_CHARS = "0123456789abcdefghijklmnopqrstuvwxyz";
    public static string GenerateRandomString(int length)

    {
        var sb = new System.Text.StringBuilder(length);
        var r = new System.Random();

        for (int i = 0; i < length; i++)

        {
            int pos = r.Next(PASSWORD_CHARS.Length);
            char c = PASSWORD_CHARS[pos];
            sb.Append(c);
        }

        return sb.ToString();
    }
    #endregion

}
