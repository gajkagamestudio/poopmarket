using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public int AnimalId;
    public bool AutoPoopActive;
    public float AutoPoopTime;

    public void AutoPooping() {
        if (AutoPoopActive) {
            GameManager.AnimalsController.AnimalPoop(this.AnimalId);

            Invoke("AutoPooping", this.AutoPoopTime);
        }
    }
}
