using UnityEngine;

namespace Monster
{
    public class GoldDropper : MonoBehaviour
    {
        [SerializeField] private MonsterEventChannelSO monsterEventChannel;
        
        private void OnEnable()
        {
            monsterEventChannel.OnEventRaised += DropGold;
        }

        private void OnDisable()
        {
            monsterEventChannel.OnEventRaised -= DropGold;
        }

        private void DropGold(MonsterController controller)
        {
            for (int i = 0; i < controller.monsterDropInfo.MonsterDropQuantity; i++)
            {
                Vector2 offset = UnityEngine.Random.insideUnitCircle * 0.3f;
                Vector2 spawnPos = (Vector2)controller.transform.position + offset;

                Instantiate(controller.monsterDropInfo.monsterDropPrefab, spawnPos, Quaternion.identity);
            }
        }
        
    }
}