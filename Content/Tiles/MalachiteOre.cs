using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Sinister.Content.Tiles
{
	public class MalachiteOre : ModTile
	{
		public override void SetStaticDefaults() {
			TileID.Sets.Ore[Type] = true; // я ебу?
			Main.tileTable[Type] = false; // Модель стола.
			Main.tileNoAttach[Type] = true; // Предотвращает присоединение блоков к этому блоку.
			Main.tileBlockLight[Type] = true; // Если включено, свет не будет проходить через блок.
			Main.tileLavaDeath[Type] = false; // Разрушает ли лава блок?
			Main.tileWaterDeath[Type] = false; // Разрушает ли вода блок?
			Main.tileCut[Type] = false; // Если включено, плитка может быть уничтожена оружием. 
			Main.tileSpelunker[Type] = true; // Плитка будет затронута выделением spelunker.
			Main.tileOreFinderPriority[Type] = 300; // Значение металлодетектора (https://terraria.gamepedia.com/Metal_Detector).
			Main.tileShine2[Type] = true; // Немного изменяет цвет рисунка.
			Main.tileShine[Type] = 600; // Как часто с этой плитки появляются мелкие пылинки. Крупнее - реже.
			Main.tileMergeDirt[Type] = true; // Если включено, блок будет сливаться с грязью.
			Main.tileSolid[Type] = true; // Солидный блок.

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Malachite ore");
			AddMapEntry(new Color(110, 168, 74), name);


			DustType = 128; // Частицы (https://terraria.fandom.com/wiki/Dust_IDs).
			ItemDrop = ModContent.ItemType<Items.Placeable.MalachiteOre>();
			HitSound = SoundID.Tink;
			// MineResist = 700f; // Не ебу.
			MinPick = 55; // Минимальная мощность кирки (https://terraria.fandom.com/wiki/Pickaxes)
		}
	}

	public class MalachiteOreSystem : ModSystem
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) {
			// Поскольку генерация мира похожа на наложение нескольких изображений друг на друга, нам нужно сделать несколько шагов между первоначальными шагами генерации мира.

			// Первый шаг - это руда. Большинство ванильных руд генерируются на шаге под названием "Shinies", поэтому для максимальной совместимости мы также сделаем это.
			// Сначала мы выясним, на каком этапе находится "Shinies".
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

			if (ShiniesIndex != -1) {
				// Далее мы вставляем наш пасс непосредственно после оригинального пасса "Shinies".
				// ExampleOrePass - это класс, показанный ниже.
				tasks.Insert(ShiniesIndex + 1, new MalachiteOrePass("Example Mod Ores", 237.4298f));
			}
		}
	}

	public class MalachiteOrePass : GenPass
	{
		public MalachiteOrePass(string name, float loadWeight) : base(name, loadWeight) {
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
			// progress.Message - это сообщение, показываемое пользователю во время выполнения следующего кода.
			// Постарайтесь, чтобы ваше сообщение было понятным. Вы можете быть немного умными, но убедитесь, что оно достаточно описательно для целей поиска и устранения неисправностей.
			progress.Message = "Example Mod Ores";

			// Руды довольно просты, мы просто используем цикл for и WorldGen.TileRunner для размещения пятен указанной плитки в мире.
			// "6E-05" - это "научная нотация". Она просто означает 0,00006, но в некотором смысле ее легче читать.
			for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++) {
				// Внутренняя часть этого цикла for соответствует одному пятну нашего Ore.
				// Сначала мы случайным образом выбираем любую координату в мире, выбирая случайные значения x и y.
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);

				// WorldGen.worldSurfaceLow - это фактически самая высокая плитка поверхности. На практике вы можете захотеть использовать WorldGen.rockLayer или другие значения WorldGen.
				int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);

				// Затем мы вызываем WorldGen.TileRunner со случайной "силой" и случайным "шагом", а также плитку, которую мы хотим разместить.
				// Не стесняйтесь экспериментировать с силой и шагом, чтобы увидеть, какую форму они генерируют.
				Tile tile = Framing.GetTileSafely(x, y);
				if (tile.HasTile && tile.TileType == TileID.Stone) {
					WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 4), WorldGen.genRand.Next(4, 5), ModContent.TileType<MalachiteOre>());
				}

				// Альтернативно, мы можем проверить плитку, уже присутствующую в интересующей нас координате.
				// Обернув WorldGen.TileRunner в следующее условие, мы заставим руду генерироваться только в Snow.

				// Tile tile = Framing.GetTileSafely(x, y);
				// if (tile.HasTile && tile.TileType == TileID.SnowBlock) {
				// 	WorldGen.TileRunner(.....);
				// }
			}
		}
	}
}