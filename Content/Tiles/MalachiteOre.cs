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
			MineResist = 700f; // Минимальная глубина появления руды.
			MinPick = 55; // Минимальная мощность кирки (https://terraria.fandom.com/wiki/Pickaxes)
		}
	}

	public class MalachiteOreSystem : ModSystem
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) {
			// Because world generation is like layering several images ontop of each other, we need to do some steps between the original world generation steps.

			// The first step is an Ore. Most vanilla ores are generated in a step called "Shinies", so for maximum compatibility, we will also do this.
			// First, we find out which step "Shinies" is.
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

			if (ShiniesIndex != -1) {
				// Next, we insert our pass directly after the original "Shinies" pass.
				// ExampleOrePass is a class seen bellow
				tasks.Insert(ShiniesIndex + 1, new MalachiteOrePass("Example Mod Ores", 237.4298f));
			}
		}
	}

	public class MalachiteOrePass : GenPass
	{
		public MalachiteOrePass(string name, float loadWeight) : base(name, loadWeight) {
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
			// progress.Message is the message shown to the user while the following code is running.
			// Try to make your message clear. You can be a little bit clever, but make sure it is descriptive enough for troubleshooting purposes.
			progress.Message = "Example Mod Ores";

			// Ores are quite simple, we simply use a for loop and the WorldGen.TileRunner to place splotches of the specified Tile in the world.
			// "6E-05" is "scientific notation". It simply means 0.00006 but in some ways is easier to read.
			for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++) {
				// The inside of this for loop corresponds to one single splotch of our Ore.
				// First, we randomly choose any coordinate in the world by choosing a random x and y value.
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);

				// WorldGen.worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.
				int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);

				// Then, we call WorldGen.TileRunner with random "strength" and random "steps", as well as the Tile we wish to place.
				// Feel free to experiment with strength and step to see the shape they generate.
				Tile tile = Framing.GetTileSafely(x, y);
				if (tile.HasTile && tile.TileType == TileID.Stone) {
					WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 4), WorldGen.genRand.Next(4, 5), ModContent.TileType<MalachiteOre>());
				}

				// Alternately, we could check the tile already present in the coordinate we are interested.
				// Wrapping WorldGen.TileRunner in the following condition would make the ore only generate in Snow.
				// Tile tile = Framing.GetTileSafely(x, y);
				// if (tile.HasTile && tile.TileType == TileID.SnowBlock) {
				// 	WorldGen.TileRunner(.....);
				// }
			}
		}
	}
}