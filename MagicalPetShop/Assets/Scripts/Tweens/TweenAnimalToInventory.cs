using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TweenAnimalToInventory : MonoBehaviour
{
    // These values are initialized from AnimalSpawnPoint which should be present in the Scene
    private float jumpDuration = 0.5f;
    private float jumpPower = 1f;

    private GameObject SpawnAnimal(Animal animal)
    {
        var spawnPoint = FindObjectOfType<AnimalSpawnPoint>();
        
        jumpDuration = spawnPoint.jumpDuration;
        jumpPower = spawnPoint.jumpPower;

        GameObject spawnedAnimal = new GameObject("spawnedAnimal");
        spawnedAnimal.transform.parent = spawnPoint.transform;
        var image = spawnedAnimal.AddComponent<Image>();
        image.sprite = animal.artwork;
        var rt = spawnedAnimal.GetComponent<RectTransform>();


        rt.sizeDelta = Vector2.one * 170f;
        rt.localScale = Vector3.one;
        rt.position = GetComponent<RectTransform>().position;
        rt.anchoredPosition = rt.anchoredPosition.WithY(rt.anchoredPosition.y - 11f);

        return spawnedAnimal;
    }

    private void TweenToInventory(GameObject animal)
    {
        var inventory = FindObjectOfType<InvButton>().gameObject;
        var rt = animal.GetComponent<RectTransform>();

        Vector3 finalPosition = inventory.GetComponent<RectTransform>().position;

        rt.DOScale(0, jumpDuration);
        rt.DOJump(finalPosition, jumpPower, 1, jumpDuration)
            .OnComplete(() => 
            {
                GameObject.Destroy(animal);
            }
            );
    }

    public void Tween(Animal animal)
    {
        TweenToInventory(SpawnAnimal(animal));
    }
}
