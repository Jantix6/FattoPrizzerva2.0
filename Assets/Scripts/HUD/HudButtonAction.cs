using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudButtonAction : MonoBehaviour
{
    private Image image;
    public List<Sprite> sprites;

    public enum State {NOTHING, RUNNING, RUNNINGHIGH, JUMP, PLANNING, INSIDEPLANT };
    public State currentState = State.NOTHING;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void ChangeState(State _newState)
    {
        if (sprites.Count >= 6)
        {
            switch (_newState)
            {
                case State.NOTHING:
                    image.sprite = sprites[0];
                    break;
                case State.RUNNING:
                    image.sprite = sprites[1];
                    break;
                case State.RUNNINGHIGH:
                    image.sprite = sprites[2];
                    break;
                case State.JUMP:
                    image.sprite = sprites[3];
                    break;
                case State.PLANNING:
                    image.sprite = sprites[4];
                    break;
                case State.INSIDEPLANT:
                    image.sprite = sprites[5];
                    break;
            }
            currentState = _newState;
        }
    }

}
