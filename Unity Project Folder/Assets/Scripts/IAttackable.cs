using UnityEngine;
using System.Collections;

public interface IAttackable
{
    float priority { get; }
    bool isLeft { get; }
    bool available { get; }
    float health { get; set; }

    void decrease(float i);
    void increase(float i);
}
