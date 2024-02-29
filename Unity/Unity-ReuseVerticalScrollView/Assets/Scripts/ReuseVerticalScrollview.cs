using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReuseVerticalScrollview<T> : MonoBehaviour
{
    public GameObject orgItemPrefab;
    public List<T> dataList = new List<T>(); //총 데이터
    public float spacing;
    private float itemLeft;
    private float itemHeight;
    [HideInInspector]
    public ScrollRect scroll;
    [HideInInspector]
    public RectTransform scrollRect;
    private List<ReuseVerticalScrollviewItem<T>> itemList = new List<ReuseVerticalScrollviewItem<T>>(); //스크롤에서 보여지는 프리팹
    private float offset;
    private void Awake()
    {
        scroll = GetComponent<ScrollRect>();
        scrollRect = scroll.GetComponent<RectTransform>();
        scroll.onValueChanged.AddListener(OnScrollValueChanged);
    }

    public virtual void InitData(List<T> list)
    {
        if (dataList != null)
            dataList.Clear();

        dataList = list;

        if (dataList.Count == 0)
            Debug.Log($"<color=red>dataList count is zero!!</color>");

        CreateItem();
    }



    protected void CreateItem()
    {
        RectTransform scrollRect = scroll.GetComponent<RectTransform>();
        if(spacing != 0)
        {
            VerticalLayoutGroup verticalLayoutGroup = scroll.content.GetComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.spacing = spacing;
        }


        itemHeight = orgItemPrefab.gameObject.GetComponent<RectTransform>().rect.height + spacing;
        itemLeft = orgItemPrefab.gameObject.GetComponent<RectTransform>().localPosition.x;

        int itemCount = (int)(scrollRect.rect.height / itemHeight) + 3; //3개만큼 더 생성
        for (int i = 0; i < itemCount; i++)
        {
            var item = Instantiate(orgItemPrefab, scroll.content);
            ReuseVerticalScrollviewItem<T> cell = item.GetComponent<ReuseVerticalScrollviewItem<T>>();
            cell.Init();

            item.transform.localPosition = new Vector3(itemLeft, -i * itemHeight);
            SetData(cell, i);
            itemList.Add(cell);
        }

        offset = itemList.Count * itemHeight;


        RemoveContentComponent();
        SetConetntHeight();
    }

    private void SetConetntHeight()
    {
        scroll.content.sizeDelta = new Vector2(scroll.content.sizeDelta.x, (dataList.Count * itemHeight) - spacing);
    }

    private bool ReLocationItem(ReuseVerticalScrollviewItem<T> item, float contentY, float scrollHeight)
    {

        if (item.transform.localPosition.y + contentY > itemHeight * 2f)
        {
            item.transform.localPosition -= new Vector3(0, offset);
            ReLocationItem(item, contentY, scrollHeight);
            return true;
        }
        else if (item.transform.localPosition.y + contentY < -scrollHeight - itemHeight)
        {
            item.transform.localPosition += new Vector3(0, offset);
            ReLocationItem(item, contentY, scrollHeight);
            return true;
        }
        return false;
    }

    private void SetData(ReuseVerticalScrollviewItem<T> item, int idx)
    {
        if (idx < 0 || idx >= dataList.Count)
        {
            item.gameObject.SetActive(false);
            return;
        }
        item.gameObject.SetActive(true);
        item.UpdateContent(dataList[idx]);
    }


    private void OnScrollValueChanged(Vector2 scrollPos)
    {
        float scrollHeight = scrollRect.rect.height;
        float contentY = scroll.content.anchoredPosition.y;

        foreach (var item in itemList)
        {
            bool isChanged = ReLocationItem(item, contentY, scrollHeight);
            if (isChanged)
            {
                int idx = (int)(-item.transform.localPosition.y / itemHeight);
                SetData(item, idx);
            }
        }
    }

    private void RemoveContentComponent()
    {
        if (scroll.content.TryGetComponent<VerticalLayoutGroup>(out VerticalLayoutGroup layoutGroup))
        {
            layoutGroup.enabled = false;
        }

        if (scroll.content.TryGetComponent<ContentSizeFitter>(out ContentSizeFitter sizeFitter))
        {
            sizeFitter.enabled = false;
        }
    }
}
