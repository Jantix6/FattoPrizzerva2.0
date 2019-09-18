using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudButtonAction : MonoBehaviour
{
    private Image image;
    public List<Sprite> sprites;
    public Sprite spriteNull;

    public enum State {NOTHING, RUNNING, RUNNINGHIGH, JUMP, PLANNING, INSIDEPLANT };
    public State currentState = State.NOTHING;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void ChangeState(State _newState)
    {
        if (sprites.Count >= 6 && image != null)
        {
            switch (_newState)
            {
                case State.NOTHING:
                    if (sprites[0] != null)
                        image.sprite = sprites[0];
                    else image.sprite = spriteNull;
                    break;
                case State.RUNNING:
                    if (sprites[1] != null)
                        image.sprite = sprites[1];
                    else image.sprite = spriteNull;
                    break;
                case State.RUNNINGHIGH:
                    if (sprites[2] != null)
                        image.sprite = sprites[2];
                    else image.sprite = spriteNull;
                    break;
                case State.JUMP:
                    if (sprites[3] != null)
                        image.sprite = sprites[3];
                    else image.sprite = spriteNull;
                    break;
                case State.PLANNING:
                    if (sprites[4] != null)
                        image.sprite = sprites[4];
                    else image.sprite = spriteNull;
                    break;
                case State.INSIDEPLANT:
                    if (sprites[5] != null)
                        image.sprite = sprites[5];
                    else image.sprite = spriteNull;
                    break;
            }
            currentState = _newState;
        }
    }

}
