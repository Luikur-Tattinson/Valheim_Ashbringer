using BepInEx;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Ashbringer
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class Mod : BaseUnityPlugin
    {
        public const string PluginGUID = "LuikurTattinson.Ashbringer";
        public const string PluginName = "ValheimAshbringer";
        public const string PluginVersion = "1.0.0";

        private AssetBundle EmbeddedResourceBundle;
        private GameObject SwordPrefab;

        private void Awake()
        {
            LoadAssets();
            CreateAshbringer();
        }

        private void LoadAssets()
        {
            Jotunn.Logger.LogInfo($"Embedded resources: {string.Join(", ", typeof(Mod).Assembly.GetManifestResourceNames())}");
            EmbeddedResourceBundle = AssetUtils.LoadAssetBundleFromResources("ashbringer");
            SwordPrefab = EmbeddedResourceBundle.LoadAsset<GameObject>("Ashbringer");
        }

        private void CreateAshbringer()
        {
            RecipeConfig recipeConfig = new RecipeConfig();
            recipeConfig.Item = "Ashbringer";
            recipeConfig.MinStationLevel = 3;
            recipeConfig.CraftingStation = "forge";
            recipeConfig.AddRequirement(new RequirementConfig("Silver", 30, amountPerLevel: 15));
            recipeConfig.AddRequirement(new RequirementConfig("ElderBark", 20, amountPerLevel: 10));
            recipeConfig.AddRequirement(new RequirementConfig("SurtlingCore", 5, amountPerLevel: 2));
            ItemManager.Instance.AddRecipe(new CustomRecipe(recipeConfig));

            CustomItem Ashbringer = new CustomItem(SwordPrefab, true);
            Ashbringer.ItemDrop.m_itemData.m_shared.m_damages.m_spirit = 40;
            Ashbringer.ItemDrop.m_itemData.m_shared.m_damages.m_slash = 90;
            Ashbringer.ItemDrop.m_itemData.m_shared.m_name = SwordPrefab.name;
            Ashbringer.ItemDrop.m_itemData.m_shared.m_description = "Blade of the Scarlet Highlord";
            ItemManager.Instance.AddItem(Ashbringer);

            //Recipe recipe = ScriptableObject.CreateInstance<Recipe>();
            //recipe.name = "Recipe_Ashbringer";
            //recipe.m_item = SwordPrefab.GetComponent<ItemDrop>();
            //recipe.m_craftingStation = Mock<CraftingStation>.Create("forge");
            //recipe.m_minStationLevel = 3;
            //var ingredients = new List<Piece.Requirement>
            //{
            //    MockRequirement.Create("Silver", 30),
            //    MockRequirement.Create("ElderBark", 20),
            //    MockRequirement.Create("SurtlingCore", 5),
            //};
            //recipe.m_resources = ingredients.ToArray();
            //CustomRecipe CR = new CustomRecipe(recipe, true, true);
            //ItemManager.Instance.AddRecipe(CR);
        }
    }
}