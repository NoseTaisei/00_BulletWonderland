using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIHealthBar : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private Image mask;

    [SerializeField]
    private Text healthText;

    private float originalSize;

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }
    void Update()
    {
        SettingHealthUI();
    }
    private void SettingHealthUI()
    {				      
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize *
         (playerManager.currentHP / playerManager.maxHP));

         healthText.text = playerManager.currentHP.ToString();
    }
}
