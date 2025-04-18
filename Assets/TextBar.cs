using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class TextBar : MonoBehaviour
{
    public string text;
    private List<GameObject> letters;
    [SerializeField] protected List<Sprite> letterSprites;

    private void Awake()
    {
        letters = new List<GameObject>();
        UpdateLetters();
    }

    private void Update()
    {
        UpdateLetters();
    }

    private GameObject CreateLetter()
    {
        GameObject letter = new GameObject();
        letter.name = "Letter";
        letter.transform.parent = transform;
        letter.transform.position = transform.position;
        letter.AddComponent<SpriteRenderer>();
        letter.AddComponent<SpriteAnimator>();
        return letter;
    }
    private Vector2 GetLetterPosition(int index)
    {
        Vector2 position = Vector2.zero;

        int length = letters.Count;
        position.x -= 0.2f * (length - 1);
        position.x += 0.5f * index;

        return position;
    }

    public void UpdateLetters()
    {
        if (transform.childCount < text.Length)
        {
            letters.Add(CreateLetter());
        }
        else if (transform.childCount > text.Length)
        {
            //delete excess letters
            letters.RemoveAt(letters.Count - 1);
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        }
            for (int i = 0; i < letters.Count; i++)
        {
            letters[i].transform.position = transform.position + (Vector3)GetLetterPosition(i) + letters[i].GetComponent<SpriteAnimator>().shakeOffset;
            letters[i].GetComponent<SpriteRenderer>().sprite = letterSprites[Board.GetIndex(text[i])];
        }
    }
}
