using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundSwapper : MonoBehaviour
{
    [Header("SpriteRenderers in the scene")]
    public SpriteRenderer backLayer;   
    public SpriteRenderer midLayer;    
    public SpriteRenderer frontLayer;  
    [Header("Sprite Pools")]
    public Sprite backPool;          
    public Sprite midPool;          
    public Sprite[] frontPool;         


    public bool useRandom = false;

    void Awake()
    {
         int masterLen = frontPool?.Length ?? 0;
        Array.Resize(ref frontPool, masterLen);
    }

    public void SetStage(int stageIndex)
    {
        if (frontPool.Length == 0) return;

        int idx = useRandom
            ? Random.Range(0, frontPool.Length)
            : stageIndex % frontPool.Length;

        backLayer.sprite = backPool;
        midLayer.sprite = midPool;
        frontLayer.sprite = frontPool[idx];

    }
}
