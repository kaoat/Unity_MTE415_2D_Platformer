public interface Character
{
    void FlipTo(string side);
    void Flip();
    void AttackTo(Character enemy, int damage);
    void Run();
    void Jump();
    void TakeDamage(int damage);
    int GetHitPoint();
    void AddHitPoint(int hp);
    void AddScore(int score);
    int GetScore();
}