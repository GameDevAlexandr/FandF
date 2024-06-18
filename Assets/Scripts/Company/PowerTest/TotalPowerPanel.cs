using UnityEngine;
using UnityEngine.UI;

public class TotalPowerPanel : MonoBehaviour
{
    [SerializeField] private Text _totalPower;
    [SerializeField] private Text _powerBefore;
    [SerializeField] private Text _resultText;

    private int _powerB;
    public void SetBefore()
    {
        _powerB = GeneralData.sonData.power;
        _powerBefore.text = _powerB.ToString();
    }
    public void Open()
    {
        gameObject.SetActive(true);
        var pw = GeneralData.sonData.power;
        _totalPower.text = pw.ToString();
        if (_powerB < pw)
        {
            _resultText.color = new Color(88, 255, 114);
            _resultText.text = "YOU HAVE INCREASED POWER";
        }
        else if(_powerB == pw)
        {
            _resultText.color = Color.white;
            _resultText.text = "YOU POWER HAS NOT CHANGED";
        }
        else
        {
            _resultText.color = new Color(255, 100, 90); 
            _resultText.text = "YOU HAVE DECREASED POWER";
        }
    }
}
