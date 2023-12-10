using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// 敵とプレイヤーに共通するステータスを管理するクラス
/// </summary>
public class Status
{
    private IntReactiveProperty _hp;
    private int _attackPower = 50;
    private int _blockPower = 1;
    public int MaxHp { get; private set; }

    public IReadOnlyReactiveProperty<int> HP => _hp;
    public int AttackPower => _attackPower;
    public int BlockPower => _blockPower;

    public Status(int hp, int attackPower, int blockPower)
    {
        _hp = new(hp);
        MaxHp = _hp.Value;
        _attackPower = attackPower;
        _blockPower = blockPower;
    }

    /// <summary>
    /// 指定したダメージを与える
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyDamage(int damage)
    {
        //計算式　残りhp =　現在のhp - (ダメージ - ブロック)
        _hp.Value -= (damage - _blockPower);
        if (_hp.Value < 0)
        {
            _hp.Value = 0;
        }
    }

    /// <summary>
    /// hpを指定した値だけ回復する
    /// </summary>
    /// <param name="hp"></param>
    public void HealHp(int hp)
    {
        _hp.Value += hp;
    }

    /// <summary>
    /// 引き数で指定した値だけ攻撃力を上げる
    /// </summary>
    /// <param name="power"></param>
    public void ApplyAttackPower(int power)
    {
        _attackPower += power;
    }

    /// <summary>
    /// 引き数で指定した値だけブロック力を上げる
    /// </summary>
    /// <param name="power"></param>
    public void ApplyBlockPower(int power)
    {
        _blockPower += power;
    }
}
