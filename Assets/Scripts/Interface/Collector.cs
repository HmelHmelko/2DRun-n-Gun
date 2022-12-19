using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private List<ICollectable> detectedCollectable = new List<ICollectable>();

    private Player player;

    [Header("Gems")]
    [SerializeField] private float gemAmount = 20.0f;
    [SerializeField] private float richGemAmount = 40.0f;
    [SerializeField] private TMP_Text gemsConterUI;
    private float gemAmountSumm;
    private CollectableGem collectableGem;

    private const string saveKey = "mainSave";

    public float gems { get { return gemAmountSumm; } }

    [Header("Keys")]
    [SerializeField] private float keyAmount = 50.0f;
    [SerializeField] private GameObject[] keys;
    public int keyCount;
    private CollectableKey collectableKey;

    protected readonly int hashActivePara = Animator.StringToHash("Active");

    private void Update()
    {
        gemsConterUI.text = gems.ToString();
    }
    public void AddToCollected(Collider2D collision)
    {
        ICollectable collectable = collision.GetComponent<ICollectable>();

        if (collectable != null)
        {
            detectedCollectable.Add(collectable);
        }
    }
    public void RemoveFromCollected(Collider2D collision)
    {
        ICollectable collectable = collision.GetComponent<ICollectable>();

        if (collectable != null)
        {
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
            AddToCollected(collision);
            CheckForCollectAmount(gemAmount);
            gemAmountSumm += gemAmount;
            RemoveFromCollected(collision);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collectableKey = collision.GetComponent<CollectableKey>();

        if (collectableKey != null && collectableKey.keyCollider.isTrigger)
        {
            AddToCollected(collision);
            CheckForCollectAmount(keyAmount);
            keys[keyCount].GetComponent<Animator>().SetBool(hashActivePara, true);
            keyCount += 1;
            gemAmountSumm += keyAmount;
            RemoveFromCollected(collision);
        }
    }
}

