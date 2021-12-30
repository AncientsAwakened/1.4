using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Melee
{
	public class TwinTalons : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("The Grips... reduced to mere gloves."); 
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 54; 
			Item.height = 42; 

			Item.useStyle = ItemUseStyleID.Swing; 
			Item.useTime = 5;
			Item.useAnimation = 5; 
			Item.autoReuse = true;

			Item.DamageType = DamageClass.Melee; 
			Item.damage = 22;
			Item.knockBack = 3; 
			Item.crit = 6; 

			Item.value = Item.buyPrice(gold: 1); 
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1; 
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(BuffID.OnFire, 60);
		}

	}
}
