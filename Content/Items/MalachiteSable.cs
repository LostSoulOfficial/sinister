using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Sinister.Content.Items
{
	public class MalachiteSable : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Malachite sable");
			Tooltip.SetDefault("This is a Malachite sable.");
		}

		public override void SetDefaults()
		{
			Item.damage = 25; // Damage.
			Item.DamageType = DamageClass.Melee; // Класс оружия.
			Item.width = 44; // Широта хитбокса.
			Item.height = 46; // Высота хитбокса.
			Item.useTime = 20; // Промежуток времени использования элемента в кадрах.
			Item.useAnimation = 20; // Промежуток времени использования анимации для элемента.
			Item.useStyle = 1; // Стиль анимации. (https://terraria.fandom.com/wiki/Use_Style_IDs)
			Item.knockBack = 6; // Отдача.
			Item.value = 10000; // Стоимость предмета
			Item.rare = 7; // Редкость предмета (https://terraria.wiki.gg/wiki/Rarity)
			Item.UseSound = SoundID.Item1; // Звук использования. (https://terraria.wiki.gg/wiki/Sound_IDs)
			Item.autoReuse = true; // Авто атака.
			Item.useTurn = true; // Позволяет двигаться во время анимации.
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}