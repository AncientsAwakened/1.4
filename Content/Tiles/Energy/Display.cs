using AAMod.Common.Globals;
using AAMod.Common.Systems;
using AAMod.Common.Systems.UI;
using AAMod.Content.Items.Placeables.Energy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Energy
{
    class Display : ModTile
    {
		internal Item item;
		public int TileX;
		public int TileY;
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(200, 200, 200), Language.GetText("Energy Charging Stand"));
		}
        public override bool RightClick(int i, int j)
        {
			if (Main.LocalPlayer.IsTileTypeInInteractionRange(Type))
            {
				Tile tile = Framing.GetTileSafely(i, j);
				TileX = i - tile.frameX / 18;
				TileY = j - tile.frameY / 18;
				UISystem system = ModContent.GetInstance<UISystem>();
				int index = system.InterfaceLayers.FindIndex(layer => layer.Name == "Vanilla: Inventory");
				system.ActivateUI(new EnergyDisplay(this), index);
				return true;
			}
			return false;
        }
        private int coolDown = 0;
		private int formula = 0;
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
			if (item != null && !item.IsAir)
			{
				Tile tile = Framing.GetTileSafely(i, j);
				if (tile.frameX == 0 && tile.frameY == 0)
                {
					Vector2 screenRange = new(Main.offScreenRange);
					Asset<Texture2D> itemTexture = TextureAssets.Item[item.type];
					spriteBatch.Draw(itemTexture.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (itemTexture.Height() >= 22 ? 22 : itemTexture.Height()) / 4, (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()), (itemTexture.Height() >= 22 ? 22 : itemTexture.Height())), Color.White);
					if (item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && ModContent.HasAsset($"AAMod/Content/Items/Weapons/Energy/{item.Name.Replace(" ", "")}_Glow"))
                    {
						Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/Energy/{item.Name.Replace(" ", "")}_Glow");
						switch (item.GetGlobalItem<AAGlobalItemModular>().OutputShard)
						{
							case 1:
								{
									Color color = new(12, 240, 21);
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (itemTexture.Height() >= 22 ? 22 : itemTexture.Height()) / 4, (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()), (itemTexture.Height() >= 22 ? 22 : itemTexture.Height())), color);
									break;
								}
							case 2:
								{
									Color color = new(30, 30, 30);
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (itemTexture.Height() >= 22 ? 22 : itemTexture.Height()) / 4, (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()), (itemTexture.Height() >= 22 ? 22 : itemTexture.Height())), color);
									break;
								}
							case 3:
								{
									Color color = new(232, 219, 36);
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (itemTexture.Height() >= 22 ? 22 : itemTexture.Height()) / 4, (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()), (itemTexture.Height() >= 22 ? 22 : itemTexture.Height())), color);
									break;
								}
							case 4:
								{
									Color color = new(195, 13, 13);
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (itemTexture.Height() >= 22 ? 22 : itemTexture.Height()) / 4, (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()), (itemTexture.Height() >= 22 ? 22 : itemTexture.Height())), color);
									break;
								}
							case 5:
								{
									Color color = new(12, 186, 187);
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (itemTexture.Height() >= 22 ? 22 : itemTexture.Height()) / 4, (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()), (itemTexture.Height() >= 22 ? 22 : itemTexture.Height())), color);
									break;
								}
							case 6:
								{
									Color color = new(234, 234, 234);
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (itemTexture.Height() >= 22 ? 22 : itemTexture.Height()) / 4, (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()), (itemTexture.Height() >= 22 ? 22 : itemTexture.Height())), color);
									break;
								}
							case 7:
								{
									Color color = new(195, 73, 13);
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (itemTexture.Height() >= 22 ? 22 : itemTexture.Height()) / 4, (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()), (itemTexture.Height() >= 22 ? 22 : itemTexture.Height())), color);
									break;
								}
							case 8:
								{
									Color color = new(226, 18, 173);
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (itemTexture.Height() >= 22 ? 22 : itemTexture.Height()) / 4, (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()), (itemTexture.Height() >= 22 ? 22 : itemTexture.Height())), color);
									break;
								}
							default:
								{
									Color color = new(23, 13, 182);
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (itemTexture.Height() >= 22 ? 22 : itemTexture.Height()) / 4, (itemTexture.Width() >= 32 ? 32 : itemTexture.Width()), (itemTexture.Height() >= 22 ? 22 : itemTexture.Height())), color);
									break;
								}
						}
					}
					if (item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1)
                    {
						switch(item.GetGlobalItem<AAGlobalItemModular>().Freedom)
                        {
							case 1:
                                {
									Color color = new(12, 240, 21);
									Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (glowMask.Width() >= 32 ? 32 : glowMask.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (glowMask.Height() >= 22 ? 22 : glowMask.Height()) / 4, (glowMask.Width() >= 32 ? 32 : glowMask.Width()), (glowMask.Height() >= 22 ? 22 : glowMask.Height())), color);
									break;
                                }
							case 2:
								{
									Color color = new(30, 30, 30);
									Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (glowMask.Width() >= 32 ? 32 : glowMask.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (glowMask.Height() >= 22 ? 22 : glowMask.Height()) / 4, (glowMask.Width() >= 32 ? 32 : glowMask.Width()), (glowMask.Height() >= 22 ? 22 : glowMask.Height())), color);
									break;
								}
							case 3:
								{
									Color color = new(232, 219, 36);
									Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (glowMask.Width() >= 32 ? 32 : glowMask.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (glowMask.Height() >= 22 ? 22 : glowMask.Height()) / 4, (glowMask.Width() >= 32 ? 32 : glowMask.Width()), (glowMask.Height() >= 22 ? 22 : glowMask.Height())), color);
									break;
								}
							case 4:
								{
									Color color = new(195, 13, 13);
									Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (glowMask.Width() >= 32 ? 32 : glowMask.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (glowMask.Height() >= 22 ? 22 : glowMask.Height()) / 4, (glowMask.Width() >= 32 ? 32 : glowMask.Width()), (glowMask.Height() >= 22 ? 22 : glowMask.Height())), color);
									break;
								}
							case 5:
								{
									Color color = new(12, 186, 187);
									Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (glowMask.Width() >= 32 ? 32 : glowMask.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (glowMask.Height() >= 22 ? 22 : glowMask.Height()) / 4, (glowMask.Width() >= 32 ? 32 : glowMask.Width()), (glowMask.Height() >= 22 ? 22 : glowMask.Height())), color);
									break;
								}
							case 6:
								{
									Color color = new(234, 234, 234);
									Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (glowMask.Width() >= 32 ? 32 : glowMask.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (glowMask.Height() >= 22 ? 22 : glowMask.Height()) / 4, (glowMask.Width() >= 32 ? 32 : glowMask.Width()), (glowMask.Height() >= 22 ? 22 : glowMask.Height())), color);
									break;
								}
							case 7:
								{
									Color color = new(195, 73, 13);
									Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (glowMask.Width() >= 32 ? 32 : glowMask.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (glowMask.Height() >= 22 ? 22 : glowMask.Height()) / 4, (glowMask.Width() >= 32 ? 32 : glowMask.Width()), (glowMask.Height() >= 22 ? 22 : glowMask.Height())), color);
									break;
								}
							case 8:
								{
									Color color = new(226, 18, 173);
									Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (glowMask.Width() >= 32 ? 32 : glowMask.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (glowMask.Height() >= 22 ? 22 : glowMask.Height()) / 4, (glowMask.Width() >= 32 ? 32 : glowMask.Width()), (glowMask.Height() >= 22 ? 22 : glowMask.Height())), color);
									break;
								}
							default:
                                {
									Color color = new(23, 13, 182);
									Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
									spriteBatch.Draw(glowMask.Value, new Rectangle((i * 16) - (int)Main.screenPosition.X + (int)screenRange.X + (glowMask.Width() >= 32 ? 32 : glowMask.Width()) / 4, (j * 16) - (int)Main.screenPosition.Y + 2 + (int)screenRange.Y + (glowMask.Height() >= 22 ? 22 : glowMask.Height()) / 4, (glowMask.Width() >= 32 ? 32 : glowMask.Width()), (glowMask.Height() >= 22 ? 22 : glowMask.Height())), color);
									break;
                                }
						}
                    }
				}
				if (item.GetGlobalItem<AAGlobalItemModular>().OutputWeapon && item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell != -1 && !item.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown)
				{
					formula++;
					if (item.GetGlobalItem<AAGlobalItemModular>().PowercellAbility(item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell, formula, true) != 0)
					{
						item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy += item.GetGlobalItem<AAGlobalItemModular>().PowercellAbility(item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell, formula, true);
						formula = 0;
					}
				}
				if (item.GetGlobalItem<AAGlobalItemModular>().OutputWeapon && !item.GetGlobalItem<AAGlobalItemModular>().CheatedIn && item.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown)
				{
					coolDown++;
					if (coolDown >= 300)
					{
						item.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = false;
						coolDown = 0;
					}
				}
				if (item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock == -1 && item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy != 0)
				{
					item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy = 0;
				}
				else if (item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy > item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock)
				{
					item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy = item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock;
				}
				if (item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy < 0)
				{
					item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy = 0;
				}
				if (!item.GetGlobalItem<AAGlobalItemModular>().Displaying)
                {
					item.GetGlobalItem<AAGlobalItemModular>().Displaying = true;
					item.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
				}
			}
			return true;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
			if (item != null && !item.IsAir)
            {
				int cool = item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant;
				int powe = item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell;
				int over = item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock;
				int scra = item.GetGlobalItem<AAGlobalItemModular>().OutputScrap;
				int shar = item.GetGlobalItem<AAGlobalItemModular>().OutputShard;
				int fra1 = item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[1 - 1];
				int fra2 = item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[2 - 1];
				int fra3 = item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[3 - 1];
				int value = Item.NewItem(i * 16, j * 16, 32, 32, item.type);
				Main.item[value].GetGlobalItem<AAGlobalItemModular>().OutputCoolant = cool;
				Main.item[value].GetGlobalItem<AAGlobalItemModular>().OutputPowercell = powe;
				Main.item[value].GetGlobalItem<AAGlobalItemModular>().OutputOverclock = over;
				Main.item[value].GetGlobalItem<AAGlobalItemModular>().OutputScrap = scra;
				Main.item[value].GetGlobalItem<AAGlobalItemModular>().OutputShard = shar;
				Main.item[value].GetGlobalItem<AAGlobalItemModular>().OutputFragment[1 - 1] = fra1;
				Main.item[value].GetGlobalItem<AAGlobalItemModular>().OutputFragment[2 - 1] = fra2;
				Main.item[value].GetGlobalItem<AAGlobalItemModular>().OutputFragment[3 - 1] = fra3;
			}
		}
    }
}
