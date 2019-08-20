using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICongelable
{
    void Congelar(bool _anim = false);
    void Descongelar(bool _anim = false);
}
