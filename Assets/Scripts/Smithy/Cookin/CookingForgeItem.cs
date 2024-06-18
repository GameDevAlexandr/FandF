using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

public class CookingForgeItem : MonoBehaviour
{
    [SerializeField] private ForgeItemType _result;
    [SerializeField] private Button _coockButton;
    [SerializeField] private Image _resultIcon;
    [SerializeField] private IngredientItem[] _ingredients;
    [SerializeField] private GameObject _queue;

    private ICookinQueue _iQueue;

    private void Awake()
    {
        _iQueue = _queue.GetComponent<ICookinQueue>();
        RecipeForge rc = CookBook.forgeItem[_result];
        _resultIcon.sprite = ForgeItemBase.Base[_result][0].icon;
        for (int i = 0; i < rc.ingredients.Length; i++)
        {
            _ingredients[i].gameObject.SetActive(true);
            RecipeForge.Ingredient ing = rc.ingredients[i];
            int sCount = !ing.isCurrency ? forgeItems[(int)ing.fItem].items[4] : currency[(int)ing.item];
            if (!ing.isCurrency)
            {
                _ingredients[i].SetData(ForgeItemBase.Base[ing.fItem][4], rc.ingredients[i].count);
            }
            else
            {
                _ingredients[i].SetData(CurrencyBase.Base[ing.item], rc.ingredients[i].count);
            }
        }
        _coockButton.onClick.AddListener(AddToQueue);
        SetInteractable();
        EventManager.ChangrForgeitem.AddListener((ForgeItemType type, int level, int val) => SetInteractable());
        EventManager.ChangeCurrency.AddListener((CurrencyType type, int val) => SetInteractable());
    }

    private void SetInteractable()
    {
        RecipeForge rc = CookBook.forgeItem[_result];
        bool isItb = true;
        for (int i = 0; i < rc.ingredients.Length; i++)
        {
            if (rc.ingredients[i].isCurrency)
            {
                if (rc.ingredients[i].count > currency[(int)rc.ingredients[i].item])
                {
                    isItb = false;
                    _ingredients[i].SetEnough(false);
                }
                else
                {
                    _ingredients[i].SetEnough(true);
                }
            }
            else
            {
                if (rc.ingredients[i].count > forgeItems[(int)rc.ingredients[i].fItem].items[4])
                {
                    isItb = false;
                    _ingredients[i].SetEnough(false);
                }
                else
                {
                    _ingredients[i].SetEnough(true);
                }
            }
        }
        _coockButton.interactable =isItb;
    }
    private void AddToQueue()
    {
        _iQueue.AddToQueue((int)_result);
    }
}
