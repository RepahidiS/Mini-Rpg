using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UIStatusBar uiStatusBar;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

public class UIBase
{
    [HideInInspector] public GameManager gameManager;
    public GameObject goParent;

    public void Show()
    {
        goParent.SetActive(true);
    }

    public void Hide()
    {
        goParent.SetActive(false);
    }
}

[System.Serializable]
public class UIStatusBar : UIBase
{
    public Text txtName;

    public Image imgHpBar;
    public Text txtHpBar;

    public Image imgMpBar;
    public Text txtMpBar;

    public Image imgExpBar;
    public Text txtExpBar;

    public Text txtLevel;

    public void UpdateName(string name)
    {
        txtName.text = name;
    }

    public void UpdateHp(int currentHp, int maxHp)
    {
        txtHpBar.text = currentHp + " / " + maxHp;
        imgHpBar.fillAmount = ((currentHp * 100.0f) / maxHp) / 100.0f;
    }

    public void UpdateMp(int currentMp, int maxMp)
    {
        txtMpBar.text = currentMp + " / " + maxMp;
        imgMpBar.fillAmount = ((currentMp * 100.0f) / maxMp) / 100.0f;
    }

    public void UpdateExp(int currentExp, int maxExp)
    {
        int percent = (currentExp * 100) / maxExp;
        txtExpBar.text = "%" + percent;
        imgExpBar.fillAmount = percent / 100.0f;
    }

    public void UpdateLevel(int level)
    {
        txtLevel.text = level.ToString();
    }
}