using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] private GameObject _highlight;


    [SerializeField] private string text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onMouseEnter() {
        _highlight.SetActive(true);
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
    }
}
