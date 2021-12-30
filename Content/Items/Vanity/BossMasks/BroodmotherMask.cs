using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Vanity.BossMasks
{
	[AutoloadEquip(EquipType.Head)]
    public class BroodmotherMask : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broodmother Mask");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			ArmorIDs.Head.Sets.DrawFullHair[Mod.GetEquipSlot(Name, EquipType.Head)] = true;
		}

		public override void SetDefaults()
        {
			Item.width = 18;
			Item.height = 10;

			Item.vanity = true;
			Item.rare = ItemRarityID.Blue;
		}
	}
}
