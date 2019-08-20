using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudButtonPunch : MonoBehaviour
{
    private Image image;
    public List<Sprite> sprites;

    public enum State { NOTHING, PUNCH, RUNPUNCH, PUNCHAEREO, ADRENALINAPUNCH, INSIDEPLANT };
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
                case State.PUNCH:
                    image.sprite = sprites[1];
                    break;
                case State.RUNPUNCH:
                    image.sprite = sprites[2];
                    break;
                case State.PUNCHAEREO:
                    image.sprite = sprites[3];
                    break;
                case State.ADRENALINAPUNCH:
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
