using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PoopController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] List<Sprite> poopSprites;

    private Image poopImage;

    private void Start()
    {
        poopImage = this.GetComponent<Image>();
        poopImage.sprite = poopSprites[Random.Range(0, poopSprites.Count)] ;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed * 100);
    }
}
