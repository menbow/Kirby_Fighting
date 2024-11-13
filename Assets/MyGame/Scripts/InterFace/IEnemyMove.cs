using System.Collections.Generic;

public interface IEnemyMove
{
    List<EnemyAction> actionData();
    float JumpPower();
    float Movespeed();
}