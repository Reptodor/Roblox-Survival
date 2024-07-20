using UnityEngine;

public class Pistol : Gun
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }        
    }
}
