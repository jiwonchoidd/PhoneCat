using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moduloBar : MonoBehaviour
{
    [SerializeField] Slider progressBar;
    [SerializeField] Text levelTier;

    [SerializeField] int maxTier = 6; 
    [SerializeField] int statValue = 0;

    [SerializeField] Button addBtn;

    
    void Start()
    {
        UpdateProgressBar();

        addBtn.onClick.AddListener(IncreaseProgress);
    }

    
    private void UpdateProgressBar()
    {
        progressBar.value = statValue % maxTier; 
        levelTier.text = Mathf.Floor(statValue / ((float)maxTier)).ToString();
    }

    
    void IncreaseProgress()
    {
        statValue++;
        UpdateProgressBar();
    }
}