namespace Assets.Scripts.Chess.Pieces
{
    public interface IHealth
    {
        float Health { get; set; }

        void GetDamage(float damage);
        void Die();
    }
}
