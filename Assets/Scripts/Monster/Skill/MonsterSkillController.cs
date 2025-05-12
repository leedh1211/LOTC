using System.Collections;
using System.Collections.Generic;
using Monster.ScriptableObject;
using UnityEngine;

namespace Monster.Skill
{
    public class MonsterSkillController : MonoBehaviour
    {
        private List<float> _cooldowns = new();
        private bool _isUsingSkill = false;
        private MonsterConfig _config;
        private Transform _target;

        public void Init(MonsterConfig config, Transform target)
        {
            _config = config;
            _target = target;
            _cooldowns.Clear();
            foreach (var skill in _config.skillData)
                _cooldowns.Add(skill.Cooldown);
        }

        public void Tick()
        {
            if (_isUsingSkill || _config == null) return;

            int selectedIndex = -1;
            float maxCooldown = -1f;

            for (int i = 0; i < _cooldowns.Count; i++)
            {
                _cooldowns[i] -= Time.deltaTime;
                if (_cooldowns[i] <= 0f && _config.skillData[i].Cooldown > maxCooldown)
                {
                    selectedIndex = i;
                    maxCooldown = _config.skillData[i].Cooldown;
                }
            }

            if (selectedIndex != -1)
            {
                StartCoroutine(UseSkillCoroutine(selectedIndex));
            }
        }

        private IEnumerator UseSkillCoroutine(int index)
        {
            _isUsingSkill = true;
            yield return _config.skillData[index].Excute(_config, transform, _target);
            _cooldowns[index] = _config.skillData[index].Cooldown;
            _isUsingSkill = false;
        }

        public bool IsUsingSkill() => _isUsingSkill;
    }
}