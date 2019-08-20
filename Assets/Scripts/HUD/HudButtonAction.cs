using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudButtonAction : MonoBehaviour
{
    public Transform transform;
    private Image image;
    public List<Sprite> sprites;

    public enum State {NOTHING, PLANTCATAPULTA, OTHERPLANT, OBJETOINTERACTUABLE };
    public State currentState = State.NOTHING;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        gameObject.transform.position = transform.position;
    }

    public void ChangeState(State _newState)
    {
        if (sprites.Count > 0)
        {
            switch (_newState)
            {
                case State.NOTHING:
                    image.sprite = sprites[0];
                    break;
                case State.PLANTCATAPULTA:
                    image.sprite = sprites[1];
                    break;
                case State.OTHERPLANT:
                    image.sprite = sprites[2];
                    break;
                case State.OBJETOINTERACTUABLE:
                    image.sprite = sprites[3];
                    break;
            }
            currentState = _newState;
        }
    }

}
