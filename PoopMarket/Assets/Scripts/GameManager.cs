using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static float money;
    public static float Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            UIController.TextMoney.text = money.ToString("#.##");
            PlayerPrefs.SetFloat("Money", money);
        }
    }

    public static UIController UIController;
    public static AnimalsController AnimalsController;

    private void Start()
    {
        UIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        AnimalsController = GameObject.FindGameObjectWithTag("AnimalsController").GetComponent<AnimalsController>();

        Money = PlayerPrefs.GetFloat("Money", 0);
    }
}
