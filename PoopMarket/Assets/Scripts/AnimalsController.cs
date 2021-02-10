using System;
using UnityEngine;

[Serializable]
public class BusterLevel {
    public int LevelNumber;
    public float LevelCost;
    public float AddingPoopCost;
}

[Serializable]
public class Buster {
    public int Id;
    public string Name;
    public bool IsActive;
    public Sprite Sprite;
    public int BusterCurrentLevel;
    public BusterLevel [] BusterLevels;
}

[Serializable]
public class AnimalBase {
    [HideInInspector] public int Id;
    public float PoopCost;
    public string AnimalName;

    public bool IsActive;
    public float AnimalCost;

    public Buster [] Busters;
}

public class AnimalsController : MonoBehaviour
{
    public AnimalBase[] Animals;
    public GameObject AnimalButtonPrefab;
    public Transform AnimalsParent;

    private void Start() {
        int index = 0;
        
        foreach (var animal in Animals) {
            animal.Id = index++;

            var newAnimalButton = Instantiate(AnimalButtonPrefab, AnimalsParent);
            var newAnimalController = newAnimalButton.GetComponent<AnimalController>();
            newAnimalController.AnimalId = animal.Id;
        }
    }

    /**
     * What happens when animal poop
     */
    public void AnimalPoop(int animalId) {
        Debug.Log("Animal poop, animal ID " + animalId);
        this.AddMoney(animalId);
    }

    private void AddMoney(int animalId) {
        float sum = Animals[animalId].PoopCost;
        foreach (var buster in Animals[animalId].Busters) {
            if (buster.IsActive) {
                sum += buster.BusterLevels[buster.BusterCurrentLevel].AddingPoopCost;
            }
        }
        
        GameManager.Money += Mathf.Round(sum * 100f) / 100f; ;
    }
}
