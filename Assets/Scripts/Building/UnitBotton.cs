using UnityEngine;
using UnityEngine.UI;

public class UnitBotton : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private Text _priceText;
    [SerializeField] private Barack _barack;


    private void Start()
    {
        _priceText.text = _unitPrefab.GetComponent<Unit>().Price.ToString();
    }
    public void TryBue()
    {
        int price = _unitPrefab.GetComponent<Unit>().Price;
        if (FindObjectOfType<Resources>().Money >= price)
        {
            FindObjectOfType<Resources>().Money -= price;
            _barack.CreateUnit(_unitPrefab);
        }
        else
        {
            Debug.Log("Мало денег");
        }
    }
}
