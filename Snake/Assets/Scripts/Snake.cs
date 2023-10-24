using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments =new List<Transform>(); //prep the list for snake segments

    private int lastKeyPress;

    public Transform segmentPrefab; //reference the segment object
    public int initialSize = 4;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (lastKeyPress != 2)
            {
              _direction = Vector2.up;
               lastKeyPress = 1; // 1 = W
            }    
        } 
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (lastKeyPress != 1)
            {
              _direction = Vector2.down;
              lastKeyPress = 2; // 2 = S
            }
 
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (lastKeyPress != 4)
            {
                _direction = Vector2.left;
                lastKeyPress = 3; // 3 = A
            }


        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (lastKeyPress != 3)
            {
                lastKeyPress = 4; // 4 = D
                _direction = Vector2.right;
            }

        }
    }

    private void FixedUpdate()
    {
        //reverse loop for the segments
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            //copies the position of whats in front of them
            _segments[i].position = _segments[i - 1].position;
            //the first segment will always follow the head
            //adding another segment will follow the first segment
            //then adding another one will follow the last added segment
        }

        //gets the current x and y position and add the new one
        //also rounding if off makes the movement in grid like
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f);

        //create a funtion for adjusting the speed
        //can be done by adjusting the time in project settings
    }

    private void Grow()
    {
        //creates a new segment
        Transform segment = Instantiate(this.segmentPrefab);

        //puts the segment to the last
        segment.position = _segments[_segments.Count - 1].position;
         
        //add the segment to the segments list
        _segments.Add(segment);
    }

    private void ResetState()
    {
        //clears the list of the segment list
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();

        //add the head back to the list
        _segments.Add(this.transform);
        //adds the additional segment 
        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }


        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
        }
        else if (collision.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
