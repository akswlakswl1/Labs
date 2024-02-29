using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReuseVerticalScrollviewItem<T> : MonoBehaviour
{
    public abstract void UpdateContent(T itemData);
    public abstract void Init();
}
