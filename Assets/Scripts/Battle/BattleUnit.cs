using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] bool isPlayer;
    [SerializeField] Sprite playerSprite; // Sprite para el jugador
    [SerializeField] Sprite dragonSprite; // Sprite para el dragón
    Image image;
    Vector3 originalPos;
    Color originalColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void PlayEnterAnimation()
    {
        if(isPlayer)
            image.transform.localPosition = new Vector3(-550f, originalPos.y);
        else
            image.transform.localPosition = new Vector3(-149f, originalPos.y);
        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        if (isPlayer)
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

        public MoveBase(string name, int power, int pp)
        {
            Name = name;
            Power = power;
        }
    }

    public List<Move> Moves { get; private set; }
public void Setup()
{
    image.sprite = isPlayer ? playerSprite : dragonSprite;

    PlayMoveAnimation();

    Moves = new List<Move>()
    {
        new Move(new MoveBase("Fire", 25, 10)),
        new Move(new MoveBase("Water", 20, 15)),
        new Move(new MoveBase("Wind", 15, 20)),
        new Move(new MoveBase("Ground", 30, 5))
    };

    image.color = originalColor;
    PlayEnterAnimation();
}
    // public bool TakeDamage(Move move)
    // {
    //     float modifiers = Random.Range(0.85f, 1.0f);
    //     float a = (2 * attacker + 10) / 250;
    //     float d = a * move.Base.Power * ((float)attacker / defense) + 2;
    //     int damage = Mathf.FloorToInt(d * modifiers);

    //     HP -= damage;
    //     if (HP <= 0)
    //     {
    //         HP = 0;
    //         return true;
    //     }

    //     return false;
    // }

    // public Move GetRandomMove() {

    //     }
}