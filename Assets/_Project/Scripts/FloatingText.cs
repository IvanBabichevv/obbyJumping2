using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float lifeTime = 0.7f;
    public float moveUpSpeed = 100f;
    public float sizeSpeed = 10f;

    [SerializeField] private TMP_Text text;

    private bool destroy;

    void Start()
    {
        StartCoroutine(DestroyDelay());
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * (moveUpSpeed * Time.deltaTime));

        if (!destroy)
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * sizeSpeed);

        if (destroy)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * sizeSpeed);

            if (transform.localScale.x < 0.1f)
                Destroy(gameObject);
        }
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(lifeTime);

        destroy = true;
    }

    public void SetText(string value)
    {
        text.text = value;
    }
}