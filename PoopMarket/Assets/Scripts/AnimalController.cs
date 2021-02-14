using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BusterLevel
{
    public int LevelNumber;
    public float LevelCost;
    public float Value;
}

[Serializable]
public class Buster
{
    public int BusterId;
    public string Name;
    public bool IsActive;
    public Sprite Sprite;
    public int BusterCurrentLevel;
    public BusterLevel[] BusterLevels;
}

[Serializable]
public class Animal
{
    public int AnimalId;
    public float PoopCost;
    public string AnimalName;
    public bool IsActive;
    public float AnimalCost;
    public float PoopHungryBarSubstractValue;
    [HideInInspector] public float HungryBarValue;
    public List<Buster> Busters;
}

public class AnimalController : MonoBehaviour
{
    public Animal Animal;

    [SerializeField] private Button animalButton;
    [SerializeField] private GameObject poopImagePrefab;
    [SerializeField] private Transform poopsParentTransform;
    [SerializeField] private Slider hungryBarSlider;
    
    private Animator animator;
    private Buster AutoPoopBuster;
    private float timer;

    private void Start()
    {
        this.GetAnimalSettings();
        this.animator = GetComponent<Animator>();
        this.AutoPoopBuster = this.Animal.Busters.Find(x => x.Name == "Auto Poop");
    }

    private void Update()
    {
        this.AutoPooping();
    }

    private void AutoPooping()
    {
        if (this.AutoPoopBuster == null)
        {
            return;
        }
        if (this.AutoPoopBuster.IsActive)
        {
            this.timer += Time.deltaTime;
        }
        if (this.timer > this.AutoPoopBuster.BusterLevels[this.AutoPoopBuster.BusterCurrentLevel].Value)
        {
            this.Poop();
            this.timer = 0;
        }
    }

    public void UpgradeBuster(Buster buster)
    {
        // TODO: upgrade buster
    }

    public void OnAnimalClick()
    {
        animator.CrossFadeInFixedTime("AnimalButtonAnimation", 0.1f);
        this.Poop();
    }

    private void Poop()
    {
        if (!this.Animal.IsActive)
        {
            return;
        }

        if (this.hungryBarSlider.value < this.Animal.PoopHungryBarSubstractValue)
        {
            this.AlertEmptyHungryBar();
            return;
        }

        this.Animal.HungryBarValue -= this.Animal.PoopHungryBarSubstractValue;
        this.hungryBarSlider.value = this.Animal.HungryBarValue;

        this.InstantiatePoop();
        this.AddMoney();

        this.SaveAnimalSettings();
    }

    private void AddMoney()
    {
        float sum = this.Animal.PoopCost;
        foreach (var buster in this.Animal.Busters)
        {
            if (buster.Name == "Auto Poop")
            {
                continue;
            }

            if (buster.IsActive)
            {
                sum += Mathf.Round(buster.BusterLevels[buster.BusterCurrentLevel].Value * 100f) / 100f;
            }
        }

        GameManager.Money += Mathf.Round(sum * 100f) / 100f;
    }

    private void SaveAnimalSettings()
    {
        PlayerPrefs.SetFloat("Animal" + this.Animal.AnimalId + "HungryBarValue", this.Animal.HungryBarValue);
        foreach (var buster in this.Animal.Busters)
        {
            PlayerPrefs.SetInt("Animal" + this.Animal.AnimalId + "Buster" + buster.BusterId + "Level", buster.BusterCurrentLevel);
            PlayerPrefs.SetInt("Animal" + this.Animal.AnimalId + "Buster" + buster.BusterId + "Active", buster.IsActive ? 1 : 0);
        }
    }

    private void GetAnimalSettings()
    {
        this.Animal.HungryBarValue = PlayerPrefs.GetFloat("Animal" + this.Animal.AnimalId + "HungryBarValue", 1f);
        this.hungryBarSlider.value = this.Animal.HungryBarValue;
        foreach (var buster in this.Animal.Busters)
        {
            buster.BusterCurrentLevel = PlayerPrefs.GetInt("Animal" + this.Animal.AnimalId + "Buster" + buster.BusterId + "Level", 0);
            buster.IsActive = PlayerPrefs.GetInt("Animal" + this.Animal.AnimalId + "Buster" + buster.BusterId + "Active", 0) == 1 ? true : false;
        }
    }

    private void InstantiatePoop()
    {
        Instantiate(poopImagePrefab, poopsParentTransform);
    }

    private void AlertEmptyHungryBar()
    {
        Debug.Log("Empty hungry bar");
    }
}
