/// <summary>
/// Interface for presenting the damage scale in the scene,
/// Component with this interface should be at the same game object with Scale 
/// </summary>
public interface IScalePresenter
{
    void PlayerDamageAdded(int amount);
    void EnemyDamageAdded(int amount);
    void SetDifference(int difference);
    void SetState(int playerAmount, int enemyAmount);

    void SetWinAmount(int amount);
    void SetLoseAmount(int amount);
}