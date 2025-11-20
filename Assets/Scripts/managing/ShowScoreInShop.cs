using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScoreInShop : MonoBehaviour
{

    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreText2;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreText2.text;
    }
}
