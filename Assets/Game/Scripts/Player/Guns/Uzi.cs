public class Uzi : Gun
{
    public override void Update()
    {
        base.Update();

        if(ShootCount > BulletInPoolCount()) 
        {
            UziOutOfBullets?.Invoke();
        }
    }
}
