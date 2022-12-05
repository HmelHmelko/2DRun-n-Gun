using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private List<ICollectable> detectedCollectable = new List<ICollectable>();

    [Header("Gems")]
    [SerializeField] private float gemAmount = 20.0f;
    [SerializeField] private TMP_Text gemsConterUI;
    private float gemAmountSumm;
    private int gemCount = 0;
    private CollectableGem collectableGem;

    public float gems { get { return gemAmountSumm; } }

    [Header("Keys")]
    [SerializeField] private float keysAmount = 50.0f;
    private int keyCount = 0;

    private void Update()
    {
        gemsConterUI.text = gems.ToString();
    }
    public void AddToCollected(Collider2D collision)
    {
        ICollectable collectable = collision.GetComponent<ICollectable>();

        if (collectable != null)
        {
            Debug.Log("add to dected" + collectable);
            detectedCollectable.Add(collectable);
        }
    }
    public void RemoveFromCollected(Collider2D collision)
    {
        ICollectable collectable = collision.GetComponent<ICollectable>();

        if (collectable != null)
        {
            Debug.Log("remove to dected" + collectable);
            detectedCollectable.Remove(collectable);
        }
    }

    public void CheckForCollectAmount(float amount)
    {
        foreach (ICollectable item in detectedCollectable)
        {
            if (item != null)
            {
                item.Collect(amount);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collectableGem = collision.GetComponent<CollectableGem>();
        
        if (collectableGem != null)
        {
            gemCount++;
            AddToCollected(collision);
            CheckForCollectAmount(gemAmount);
            gemAmountSumm += gemAmount;
            RemoveFromCollected(collision);
        }
    }
}
