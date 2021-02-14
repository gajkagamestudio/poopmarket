using System.Collections.Generic;
using UnityEngine;

public class AnimalsController : MonoBehaviour
{
    public int ActiveAnimalIndex; // Animal which is currently on display
    public List<AnimalController> Animals;

    [SerializeField] RectTransform AnimalsHolder;
    [SerializeField] float swipeSpeed;

    private Vector2 newPosition;

    private void Start()
    {
        var animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (var animal in animals)
        {
            this.Animals.Add(animal.GetComponent<AnimalController>());
        }
    }

    private void Update()
    {
        this.AnimalsHolder.anchoredPosition = 
            Vector2.Lerp(
                this.AnimalsHolder.anchoredPosition, 
                new Vector2(
                    Screen.width * this.ActiveAnimalIndex * -1f, 
                    this.AnimalsHolder.anchoredPosition.y), 
                Time.deltaTime * swipeSpeed);
    }

    public void SwipeLeft()
    {
        if (this.ActiveAnimalIndex == 0)
        {
            return;
        }

        SelectAnimal(true);
    }

    public void SwipeRight()
    {
        if (this.ActiveAnimalIndex == this.Animals.Count - 1)
        {
            return;
        }

        SelectAnimal(false);
    }

    private void SelectAnimal(bool left)
    {
        this.ActiveAnimalIndex += left ? -1 : 1;
    }
}
