using UnityEngine;

public class TavernController : MonoBehaviour
{
    public GameObject InvestmentPanel;

    public void OpenInvestmentPanel()
    {
        InvestmentPanel.SetActive(true);
    }

    public void CloseInvestmentPanel()
    {
        InvestmentPanel.SetActive(false);
    }
}