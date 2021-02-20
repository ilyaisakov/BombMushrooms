using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllBombs : MonoBehaviour
{
    [SerializeField] Image imageBombs;
    [SerializeField] TextMeshProUGUI textBombsCount;
    public int currentBomb = 0;
    [SerializeField] List<Sprite> bombList_sprites = new List<Sprite>();
    public List<int> bombList_counts = new List<int>();

    void Start()
    {
        SetCurrentBombUI();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentBomb == bombList_sprites.Count - 1)
            {
                currentBomb = 0;
            }
            else
            {
                currentBomb++;
            }
            SetCurrentBombUI();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentBomb == 0)
            {
                currentBomb = bombList_sprites.Count - 1;
            }
            else
            {
                currentBomb--;
            }
            SetCurrentBombUI();
        }
    }



    public void SetCurrentBombUI()
    {
        imageBombs.sprite = bombList_sprites[currentBomb];
        textBombsCount.text = bombList_counts[currentBomb].ToString();
    }
}
