using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GeneralData;

[RequireComponent(typeof(Image))]
public class MergeItems : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    public ForgeItem item;
    public ForgeItemStorage storage;

    [SerializeField] private CanvasGroup _cGroup;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _back;
    [SerializeField] private Image _selectImg;

    private Transform _moveableObj;

    private void Start()
    {
        EventManager.SelectMergeItem.AddListener(Select);
        _moveableObj = _cGroup.transform;
    }
    private void Select(MergeItems itm, bool isSelect)
    {
        if (!item)
        {
            return;
        }
        if (itm != this && item.type == itm.item.type && item.level == itm.item.level && !item.isLast)
        {
            _selectImg.enabled = isSelect;
        }
        else
        {
            _selectImg.enabled = false;
        }
    }

    public void SetData(ForgeItem fItem)
    {
        item = fItem;
        _cGroup.gameObject.SetActive(true);
        _icon.sprite = item.icon;
        _frame.sprite = RarityBase.frames[item.level];
        _back.sprite = RarityBase.backs[item.material];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item)
        {
            EventManager.SelectMergeItem.Invoke(this, true);
            _moveableObj.parent = transform.parent.parent;
            _cGroup.blocksRaycasts = false;
            _cGroup.alpha = 0.8f;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!item) return;
        _moveableObj.position = transform.position;
        _moveableObj.parent = transform;
        _cGroup.blocksRaycasts = true;
        _cGroup.alpha = 1f;
        EventManager.SelectMergeItem.Invoke(this, false);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(item) _moveableObj.position = eventData.position;        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item) return;

        MergeItems mItem;
        if(mItem = eventData.pointerDrag.GetComponent<MergeItems>())
        {
            if(mItem!=this && item.type == mItem.item.type && item.level == mItem.item.level && !item.isLast)
            {                
                forgeItems[(int)item.type].items[item.level]-=2;
                forgeItems[(int)item.type].items[item.level + 1]++;
                EventManager.AddForgeItem(item.type, item.level+1, 0);
                SetData(ForgeItemBase.Base[item.type][item.level + 1]);
                storage.RemoveItem(mItem);
                Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.minePreview);
            }
        }
        EventManager.SelectMergeItem.Invoke(this, false);
    }

    public void EmptyCell()
    {
        item = null;
        _cGroup.gameObject.SetActive(false);
        transform.SetAsLastSibling();
    }
    private void OnDestroy()
    {
        Destroy(_back.gameObject);
    }
}
