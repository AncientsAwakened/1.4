using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Melee
{
	public class YoinkySploinky : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Yoinky Sploinky");
			Tooltip.SetDefault("Go shank a bitch" + "\nMade in London" + "\nbababooey");
		}
		public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.DamageType = DamageClass.Melee;

			Item.damage = 69420;
            Item.knockBack = 666f;

			Item.width = 46;
			Item.height = 56;

            Item.useTime = Item.useAnimation = 2;
            Item.useStyle = ItemUseStyleID.Thrust;

            Item.UseSound = SoundID.Item1;
			Item.rare = ItemRarityID.Expert;

			Item.value = Item.sellPrice(copper: 14);
        }

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) => target.AddBuff(BuffID.Daybreak, 180);
	}
}
