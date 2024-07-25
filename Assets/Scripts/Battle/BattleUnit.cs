using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    public string unitName;
    [SerializeField] bool isPlayerUnit;
    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite dragonSprite;
    Image image;
    Vector3 originalPos;
    Color originalColor;
    public int maxHP = 30;
    public int currentHP = 30;

    public ParticleSystem fireParticle;
    public ParticleSystem waterParticle;
    public ParticleSystem windParticle;
    public ParticleSystem groundParticle;
    public ParticleSystem breathfireParticle;
    public ParticleSystem clawsParticle;


    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void PlayEnterAnimation()
    {
        if(isPlayerUnit)
            image.transform.localPosition = new Vector3(-550f, originalPos.y);
        else
            image.transform.localPosition = new Vector3(550f, originalPos.y);
            image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        if (isPlayerUnit)
        sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
        else
        sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));

        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 200f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }

    [System.Serializable]
    public class Move
    {
        public MoveBase Base { get; set; }
        public Move(MoveBase pBase)
        {
            Base = pBase;
        }
    }

[System.Serializable]
public class MoveBase
{
    public string Name { get; set; }
    public int Power { get; set; }
    public string Type { get; set; }

    public MoveBase(string name, int power, string type)
    {
        Name = name;
        Power = power;
        Type = type;
    }
}
public List<Move> Moves { get; private set; }
public void Setup()
{
    image.sprite = isPlayerUnit ? playerSprite : dragonSprite;
    PlayEnterAnimation();
    
    if (isPlayerUnit)
    {
        Moves = new List<Move>()
        {
            new Move(new MoveBase("Flama", 5, "Fire")),
            new Move(new MoveBase("Aqua Doble", 3, "Water")),
            new Move(new MoveBase("Torbellino", 4, "Wind")),
            new Move(new MoveBase("Espora", 2, "Ground")),
        };
    }
    else
    {
        Moves = new List<Move>()
        {
            new Move(new MoveBase("Garras", 6, "Claws")),
            new Move(new MoveBase("Aliento de fuego", 7, "BreathFire")),
        };
    }

    image.color = originalColor;
}

public void ActivateParticle(string moveType)
{
    switch (moveType)
    {
        case "Fire":
            fireParticle.Play();
            break;
        case "Water":
            waterParticle.Play();
            break;
        case "Wind":
            windParticle.Play();
            break;
        case "Ground":
            groundParticle.Play();
            break;
        case "BreathFire":
            breathfireParticle.Play();
            break;
        case "Claws":
            clawsParticle.Play();
            break;
    }
}
public bool TakeDamage(Move move)
{
    int damage = move.Base.Power;

    currentHP -= damage;
    PlayHitAnimation();
    if(currentHP <= 0)
    {
        PlayFaintAnimation();
        return true;
    }
    else
    {
        return false;
    }
}
}