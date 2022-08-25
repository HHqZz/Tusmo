using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile2 : MonoBehaviour
{

    [SerializeField] private GameObject _hide;
    [SerializeField] private GameObject _visible;



    // Start is called before the first frame update
    void Start()
    {
        _hide.SetActive(true);
        _visible.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMouseEnter() {
        _hide.SetActive(false);
        _visible.SetActive(true);

    }

    public void OnMouseExit() {
        _hide.SetActive(true);
        _visible.SetActive(false);
    }

}
