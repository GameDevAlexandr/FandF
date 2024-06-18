using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

public class CookingItem : MonoBehaviour
{
    [SerializeField] private CurrencyType _result;
    [SerializeField] private Button _coockButton;
    [SerializeField] private Image _resultIcon;
    [SerializeField] private IngredientItem[] _ingredients;
    [SerializeField] private GameObject _queue;

    private ICookinQueue _iQueue;

    private void Awake()
    {
        _iQueue = _queue.GetComponent<ICookinQueue>();
        Recipe rc = CookBook.item[_result];
        _resultIcon.sprite = CurrencyBase.Base[_result].icon;
        for (int i = 0; i < rc.ingredients.Length; i++)
        {
            _ingredients[i].gameObject.SetActive(true);
            _ingredients[i].SetData(CurrencyBase.Base[rc.ingredients[i].item], rc.ingredients[i].count);
        }
        _coockButton.onClick.AddListener(AddToQueue);
        SetInteractable();
        EventManager.ChangeCurrency.AddListener((CurrencyType type, int val) => SetInteractable());
    }

    private void SetInteractable()
    {
        Recipe rc = CookBook.item[_result];
        bool isItb = true;
        for (int i = 0; i < rc.ingredients.Length; i++)
        {
            if(rc.ingredients[i].count> currency[(int)rc.ingredients[i].item])
            {
                isItb = false;
                _ingredients[i].SetEnough(false);
            }
            else
            {
                _ingredients[i].SetEnough(true);
            }
        }
        _coockButton.interactable =isItb;
    }
    private void AddToQueue()
    {
        _iQueue.AddToQueue((int)_result);
    }
}
