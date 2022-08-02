using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 using UnityEngine.UI;

// [CreateAssetMenu(menuName = "Item Slots")]
public class itemSlot : MonoBehaviour
{
    [SerializeField]
    private Image m_icon;
    [SerializeField]
    private TMP_Text m_label;
    [SerializeField]
    private TMP_Text m_stackLabel;
    [SerializeField]
    private GameObject m_stackObj;

    public void Set(inventoryItem item)
    {
        m_icon.sprite = item.data.icon;
        m_label.text = item.data.displayName;
        if(item.stackSize <= 1)
        {
            m_stackObj.SetActive(false);
            return;
        }

        m_stackLabel.text = item.stackSize.ToString();
    }

}
