using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonFoundation.Geography;
using CommonFoundation.Models;
using Microsoft.CodeAnalysis.CSharp;

namespace ApiForClients.Repositories
{
    public class RepositoryMockup
    {
        public static class DefineClass
        {
            public const int nTempListCount = 15;
        }

        static List<GoodsItem> _goodsItems = new List<GoodsItem>();

        public static string GetDescription(ShopItem item)
        {
            // Summarize products of the shop
            // for example, top three products that the owner selceted as hit items.
            return item.ShopInfo.ShopName + " 의 3가지 히트상품(견본)";
        }

        public static List<GoodsItem> GetGoodsList(SearchParameters parameter)
        {
            Random random = new Random((int)DateTime.Now.ToBinary());
            // sample code;
            GetShopList(parameter);

            _goodsItems.Clear();

            for (int iShop = 0; iShop < DefineClass.nTempListCount; iShop++)
            {
                for (int iGoods = 0; iGoods < DefineClass.nTempListCount; iGoods++)
                {
                    int originalPrice = random.Next(5000, 10000);
                    int discountPrice = originalPrice -
                                        random.Next((int)(originalPrice * 0.2), (int)(originalPrice * 0.8));
                    _goodsItems.Add(new GoodsItem(
                        new GoodsInfo(iShop + 1, iGoods + 1, "Test Goods Item " + iGoods.ToString(), originalPrice, discountPrice),
                        "TodaySalesApp.Images.default_product.png",
                        DateTime.Now.AddHours(iGoods), DateTime.Now.AddHours(iGoods + 1), 1000));
                }
            }

            return _goodsItems;
        }

        public static GoodsItem[] GetGoodsList(long shopId)
        {
            GoodsItem[] items = _goodsItems.FindAll(item => item.Info.ShopId == shopId).ToArray();
            return items;
        }

        static List<ShopItem> _shopItems = new List<ShopItem>();

        public static DetailedShopItem GetDetailedShopInformation(long shopId)
        {
            //Todo : get shop detailed information from server;
            ShopItem shopItem = _shopItems.Find(item => item.ShopInfo.Id == shopId);

            GoodsItem[] goodsItems = GetGoodsList(shopId);
            EvaluationItem[] evaluationItems = GetEvaluationList(shopId);

            return new DetailedShopItem(shopItem, goodsItems, evaluationItems);
        }

        public static List<ShopItem> GetShopList(SearchParameters parameter)
        {
            Random random = new Random((int)DateTime.Now.ToBinary());

            _shopItems.Clear();

            for (int i = 0; i < DefineClass.nTempListCount; i++)
            {
                _shopItems.Add(new ShopItem(
                    new ShopInfo(i + 1, "Test Shop " + i.ToString(), "서울 특별시" + i.ToString(),
                        "02-1234-123" + i.ToString(), new GeoPoint(37.554690 + random.NextDouble() / 1000, 126.970702 + random.NextDouble() / 1000)),
                    "TodaySalesApp.Images.default_shop.png", DateTime.Now.AddDays(i), DateTime.Now.AddDays(i + 1),
                    1000));
            }

            return _shopItems;
        }

        public static EvaluationItem[] GetEvaluationList(long shopId)
        {
            Random random = new Random((int)DateTime.Now.ToBinary());

            List<EvaluationItem> items = new List<EvaluationItem>();

            for (int i = 0; i < DefineClass.nTempListCount; i++)
            {
                items.Add(new EvaluationItem()
                {
                    ShopId = shopId,
                    StarCount = random.Next(0, 5),
                    Description = "not supported yet"
                });
            }

            return items.ToArray();
        }
    }
}
