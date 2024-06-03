using GameManagement;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class XpBar : MonoBehaviour
    {
        [SerializeField] LevelUpManager levelUpManager;
        [SerializeField] private Image blueXpBar;
        [SerializeField] private TextMeshProUGUI lvlText;

        
        private void Start()
        {
            levelUpManager.OnXpGain += UpdateXpBar;
            UpdateXpBar();
        }

        private void UpdateXpBar()
        {
            blueXpBar.fillAmount = (float)levelUpManager.currentXp / levelUpManager.xpToNextLevel;
            lvlText.text =  $"Lvl: {levelUpManager.playerLevel}";
        }
    }
}
