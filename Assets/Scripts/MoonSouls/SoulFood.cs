using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class SoulFood : MonoBehaviour
{
    public SoulCell cell { get; private set; }
    
    [SerializeField] private Image _icon;
    [SerializeField] private Image _lock;

    public void SetData(SoulCell cell)
    {
        this.cell = cell;
        _icon.enabled = cell!=null;
        if (_icon.enabled)
        {
            _icon.sprite = SoulsBase.IndexBase[souls[cell.dataIndex].index].icon;
        }
    }
    public void SetLock(bool isOpen)
    {
        _icon.enabled = false;
        _lock.enabled = !isOpen;
        cell = null;
    }
}
