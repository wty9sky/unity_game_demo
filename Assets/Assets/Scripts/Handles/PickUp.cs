using UnityEngine;
using DG.Tweening;


public class PickUp : MonoBehaviour
{
    public enum PickUpType{
        Coin,
        HealthingPotion,
    }


    [Header("道具类型")]
    [SerializeField] private PickUpType pickupType;
    [SerializeField] private int value;


    [Header("拾取动画")]
    // [SerializeField] private float throwForce;
    [SerializeField] private float throwHeight = 1f;
    [SerializeField] private float throwDuration = 1f;


    [Header("拾取范围")]
    [SerializeField] private float pickUpDistance = 3f;
     private bool canPickUp = false;
     
     [SerializeField] private float itemMoveSpeed = 5f;



    private Player player;


    private void Start() {
        ThrowItem();
        // // transform.DOMove(transform.position + Vector3.up * 0.5f, 1f).SetLoops(-1, LoopType.Yoyo);
        // // transform.DOMove(transform.position + new Vector3(0, 1f, 0), 1f);
        // transform.DOMove(transform.position + new Vector3(0, 1f, 0), 1f).SetEast(Ease.InOutCubic).OnComplete(()=>{
             
        // })
    }

    private void ThrowItem(){
        transform.DOJump(transform.position,throwHeight,1,throwDuration)
        .OnComplete(()=>{
            canPickUp = true;
        });
    }

    private void Update() {
        if(canPickUp && Vector2.Distance(transform.position, player.transform.position) < pickUpDistance){
            Vector2 dir = (player.transform.position - transform.position).normalized;
            transform.Translate(dir * Time.deltaTime * itemMoveSpeed);
            // float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            // if(distanceToPlayer <= pickUpDistance){
            //     CollectPickUp();
            // }
        }
    }

    private void Awake() {
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(canPickUp && collision.gameObject.GetComponent<Player>()){
            CollectPickUp();
        }
    }

    private void CollectPickUp(){
        switch (pickupType){
            case PickUpType.Coin:
                HandleCoinPickUp();
            // 处理金币逻辑
                break;
            case PickUpType.HealthingPotion:
                HandleHealthingPotionPickUp();
            // 处理回血道具逻辑 
                break;
        }
        Destroy(gameObject);
    }


    private void HandleCoinPickUp(){
        // 增加玩家金币数量
        GameManager.Instance.UpdateCoin(value);
        GameManager.Instance.ShowText("+" + value,transform.position,Color.yellow);

    }
    private void HandleHealthingPotionPickUp(){
        // 增加玩家生命值
        player.RestoreHealth(value);
        GameManager.Instance.ShowText("+" + value,transform.position,Color.green);
    }
}
