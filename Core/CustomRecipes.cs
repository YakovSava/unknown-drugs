using System.Collections.Generic;
using CUCoreLib.Registries;
using UnknownDrugs.Items;

namespace UnknownDrugs.Core
{
    public static class CustomRecipes
    {
        public const int Count = 6;

        private const string BiochemId = "biochem";
        private const float PsilocybeYieldMl = 100f;
        private const float BiochemForPsilocybeMl = 20f;
        private const int PsilocybeInt = 5;
        private const float VasopressinLeptinYieldMl = 100f;
        private const float DdvapMl = 40f;
        private const float TrazodoneMl = 40f;
        private const float BiochemForVasopressinLeptinMl = 20f;
        private const int VasopressinLeptinInt = 10;

        public static void Register()
        {
            RecipeRegistry.Register(CreatePsilocybeRecipe());
            RecipeRegistry.Register(CreateVasopressinLeptinRecipe());
            RecipeRegistry.Register(CreateGoofballRecipe());
            RecipeRegistry.Register(CreateTranqRecipe());
            RecipeRegistry.Register(CreateKrokodilRecipe());
            RecipeRegistry.Register(CreateSpeedballRecipe());
        }

        private static Recipe CreatePsilocybeRecipe()
        {
            return new Recipe
            {
                INT = PsilocybeInt,
                category = Recipes.RecipeCategory.Medicine,
                result = new RecipeResult
                {
                    id = "psilocybin",
                    isLiquid = true,
                    resultCondition = PsilocybeYieldMl
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0f)
                    {
                        specific = true,
                        specificId = PsilocybeFungus.ItemId
                    },
                    new RecipeItem(BiochemForPsilocybeMl)
                    {
                        specific = true,
                        specificId = BiochemId,
                        isLiquid = true
                    }
                }
            };
        }

        private static Recipe CreateGoofballRecipe()
        {
            return CreateMixtureRecipe(
                "goofball",
                100f,
                8,
                new RecipeItem(50f)
                {
                    specific = true,
                    specificId = "pervitin",
                    isLiquid = true
                },
                new RecipeItem(50f)
                {
                    specific = true,
                    specificId = "heroin",
                    isLiquid = true
                });
        }

        private static Recipe CreateTranqRecipe()
        {
            return CreateMixtureRecipe(
                "tranq",
                50f,
                12,
                new RecipeItem(5f)
                {
                    specific = true,
                    specificId = "fentanyl",
                    isLiquid = true
                },
                new RecipeItem(45f)
                {
                    specific = true,
                    specificId = "xylazine",
                    isLiquid = true
                });
        }

        private static Recipe CreateKrokodilRecipe()
        {
            return CreateMixtureRecipe(
                "krokodil",
                100f,
                4,
                new RecipeItem(50f)
                {
                    specific = true,
                    specificId = "desomorphine",
                    isLiquid = true
                },
                new RecipeItem(50f)
                {
                    specific = true,
                    specificId = "bleach",
                    isLiquid = true
                });
        }

        private static Recipe CreateSpeedballRecipe()
        {
            return CreateMixtureRecipe(
                "speedball",
                100f,
                8,
                new RecipeItem(50f)
                {
                    specific = true,
                    specificId = "cocaine",
                    isLiquid = true
                },
                new RecipeItem(50f)
                {
                    specific = true,
                    specificId = "desomorphine",
                    isLiquid = true
                });
        }

        private static Recipe CreateMixtureRecipe(
            string resultId,
            float resultMl,
            int intelligence,
            params RecipeItem[] items)
        {
            return new Recipe
            {
                INT = intelligence,
                category = Recipes.RecipeCategory.Medicine,
                result = new RecipeResult
                {
                    id = resultId,
                    isLiquid = true,
                    resultCondition = resultMl
                },
                items = new List<RecipeItem>(items)
            };
        }

        private static Recipe CreateVasopressinLeptinRecipe()
        {
            return new Recipe
            {
                INT = VasopressinLeptinInt,
                category = Recipes.RecipeCategory.Medicine,
                result = new RecipeResult
                {
                    id = "vasopressinleptin",
                    isLiquid = true,
                    resultCondition = VasopressinLeptinYieldMl
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(DdvapMl)
                    {
                        specific = true,
                        specificId = "ddvap",
                        isLiquid = true
                    },
                    new RecipeItem(TrazodoneMl)
                    {
                        specific = true,
                        specificId = "trazodone",
                        isLiquid = true
                    },
                    new RecipeItem(BiochemForVasopressinLeptinMl)
                    {
                        specific = true,
                        specificId = BiochemId,
                        isLiquid = true
                    }
                }
            };
        }
    }
}
