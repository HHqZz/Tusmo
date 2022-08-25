using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    Vector3 touchPosWorld;
 
    TouchPhase touchPhase = TouchPhase.Ended;
 
    void Update() {

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase) || Input.GetMouseButtonDown(0)) {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null) {
                GameObject touchedObject = hitInformation.transform.gameObject;
                Debug.Log("Touched " + touchedObject.transform.name);
                if (touchedObject is Tile2) {
                    var my_tile = touchedObject.GetComponent<Tile2>();
                    my_tile.OnMouseEnter();
                }
            }
        }
    }
}
