using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text _text = null;
    float _money = 0;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = $"Money : " + _money.ToString();
    }

    public void AddMoney()
    {
        _money += 1f;
    }

    public float Money
    {
        get { return _money; }
        set { _money = value; }
    }
}
