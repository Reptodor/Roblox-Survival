using UnityEngine;

public class Booster : MonoBehaviour
{
    private Player _player;
    private int _index;
    private float _boostMultiplier;
    private float _boostTime;

    public void Initialize(Player player, int index,float bustMultiplier, float boostTime)
    {
        _player = player;
        _index = index;
        _boostMultiplier = bustMultiplier;
        _boostTime = boostTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _player.gameObject)
        {
            _player.Boost(_index, _boostMultiplier, _boostTime);
            this.gameObject.SetActive(false);
        }
    }
}
