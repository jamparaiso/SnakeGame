using System.Security.Cryptography;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        //generate a random position within the boundds of the gridarea
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x,bounds.max.x);
        float y = Random.Range(bounds.min.y,bounds.max.y);

        //position the food the grid
        this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round(y),0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" )
        {
            RandomizePosition();
        }

        if (collision.tag == "Obstacle")
        {
            RandomizePosition();
        }
    }
}
