using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sinister.Content.NPCs
{
    public class CustomSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("CustomSlime");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime]; // Установим количество кадров анимации равным количеству у обычного слизня
        }

        public override void SetDefaults()
        {
            NPC.width = 32; // Ширина NPC
            NPC.height = 24; // Высота NPC
            NPC.damage = 1; // Урон NPC
            NPC.defense = 2; // Защита NPC
            NPC.lifeMax = 9999; // Максимальное количество здоровья NPC
            NPC.aiStyle = 1; // Стиль искусственного интеллекта NPC (здесь обычный слизень)
            NPC.knockBackResist = 0.5f; // Сопротивление NPC откидыванию при получении удара
            AnimationType = NPCID.BlueSlime; // Установим анимацию равной анимации у обычного слизня
            NPC.HitSound = SoundID.ResearchComplete; // Звук получения удара NPC (https://terraria.wiki.gg/wiki/Sound_IDs)
            NPC.DeathSound = SoundID.NPCDeath60; // Звук смерти NPC (https://terraria.wiki.gg/wiki/Sound_IDs)
            NPC.value = Item.buyPrice(0, 0, 1, 0); // Стоимость продажи NPC
            NPC.lavaImmune = true; // NPC не получает урон от лавы
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (true) {
                return 8f; // Шанс спавна NPC равен шансу спавна обычного слизня в дневное время, умноженному на 0.5
            }
        }
    }
}
