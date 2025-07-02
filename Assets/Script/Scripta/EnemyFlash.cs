using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlash : MonoBehaviour
{
    public static EnemyFlash instance;
    private SpriteRenderer renderer;
    public Color flashColor= Color.red;
    private Color originalColor;
    public float duratn = .2f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        originalColor = renderer.color;
    }

 public void Flash()
    {
     //   StopAllCoroutines();
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        renderer.color = flashColor;
        yield return new WaitForSeconds(duratn);
        renderer.color = originalColor;
    }
}
