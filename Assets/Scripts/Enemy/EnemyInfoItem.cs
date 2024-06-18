using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

[RequireComponent(typeof(Button))]
public class EnemyInfoItem : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic _icon;
    [SerializeField] private EnemyInfoPanel _panel;
    [SerializeField] private GameObject _hideSprite;

    private Button _button;
    private EnemyItem _item;
    
    public void SetData(EnemyItem item)
    {
        if (!_button)
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(CallPanel);
        }
        _item = item;
        bool isOpen = false; 
        for (int i = 0; i < almanac.Count; i++)
        {
            if(item.enemyName == almanac[i].eName && almanac.Count > 0)
            {
                isOpen = true;
                break;
            }
        }
        
        if (isOpen)
        {
            _icon.skeletonDataAsset = item.icon;
            _icon.Initialize(true);            
        }
        _icon.gameObject.SetActive(isOpen);
        _button.interactable = isOpen;
        _hideSprite.SetActive(!isOpen);
    }

    private void CallPanel()
    {
        
        _panel.gameObject.SetActive(true);
        _panel.SetData(_item);
    }
}
