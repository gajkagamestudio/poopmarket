using UnityEngine;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    private int animalId;
    [HideInInspector] public int AnimalId 
    { 
        get
        {
            return this.animalId;
        } 
        set
        {
            this.animalId = value;

            this.AutoPoopActive = true;
            this.AutoPoopTime = 2f;

            animalButton.onClick.AddListener(Poop);
            InvokeRepeating("AutoPooping", 0f, this.AutoPoopTime);
        }
    }

    [HideInInspector] public bool AutoPoopActive;
    [HideInInspector] public float AutoPoopTime;

    [SerializeField] Button animalButton;

    public void AutoPooping()
    {
        if (this.AutoPoopActive)
        {
            GameManager.AnimalsController.AnimalPoop(this.AnimalId);
        }
    }

    private void Poop()
    {
        GameManager.AnimalsController.AnimalPoop(this.AnimalId);
    }
}
