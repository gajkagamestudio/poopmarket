using System;
using UnityEngine;

[Serializable]
public class Buster {
    public int Id;
    public string Name;
    public bool IsActive;
    public float BusterCost;
    public float AddingPoopCost;
    public Sprite Sprite;
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
            var newAnimalController = newAnimalButton.AddComponent<AnimalController>();
            newAnimalController.AnimalId = animal.Id;

            foreach (var buster in animal.Busters) {
                if (buster.Name.Equals("AutoPoop") && buster.IsActive) {
                    newAnimalController.AutoPoopActive = true;
                    newAnimalController.AutoPoopTime = 2f;
                    newAnimalController.AutoPooping();
                }
            }
        }
    }

    /**
     * What happens when user clicks on animal
     */
    public void AnimalClick(int animalId) {
        Debug.Log("Click on animal ID " + animalId);
        this.AnimalPoop(animalId);
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
                sum += buster.AddingPoopCost;
            }
        }
        
        GameManager.Money += sum;
    }
}
