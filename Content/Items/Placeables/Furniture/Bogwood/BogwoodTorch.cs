using AAMod.Content.Items.Bases;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodTorch : TileItem<Tiles.Furniture.Bogwood.BogwoodTorch>
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mire Torch");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SafeSetDefaults()
		{
			Item.noWet = true;
			Item.flame = true;

			Item.maxStack = 99;

			Item.holdStyle = ItemHoldStyleID.HoldFront;

			Item.width = 14;
			Item.height = 16;

			Item.consumable = true;
		}

		public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
		{
			itemGroup = ContentSamples.CreativeHelper.ItemGroup.Torches;
		}

		public override void HoldItem(Player player)
		{
			if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
			{
				Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, DustID.Flare);
			}

			Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);

			Lighting.AddLight(position, 0.7f, 0.3f, 0f);
		}

		public override void PostUpdate()
		{
			if (!Item.wet)
			{
				Lighting.AddLight(Item.Center, 0.7f, 0.3f, 0f);
			}
		}

		public override void AutoLightSelect(ref bool dryTorch, ref bool wetTorch, ref bool glowstick)
		{
			dryTorch = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(3).AddIngredient(ItemID.Torch, 3).AddIngredient(ModContent.ItemType<Blocks.Bogwood>()).Register();
		}
	}
}
