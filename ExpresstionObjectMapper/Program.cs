using System;
using System.Diagnostics;
using System.Linq.Expressions;
using ExpresstionObjectMapper.Clones;
using ExpresstionObjectMapper.Models;

namespace ExpresstionObjectMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            const int count = 1000000;
            SourceModel source = new SourceModel()
            {
                ID = Guid.NewGuid(),
                Name = "Source Model Instance",
                Amount = 100_0000,
                AuctionPrice = 110,
                BiddingPrice = 90,
                BuyPrice1 = 91,
                BuyPrice2 = 92,
                BuyPrice3 = 93,
                BuyPrice4 = 94,
                BuyPrice5 = 95,
                BuyStrand1 = 1000,
                BuyStrand2 = 2000,
                BuyStrand3 = 3000,
                BuyStrand4 = 4000,
                BuyStrand5 = 5000,
                ClosingPriceYesterday = 95,
                Count = 10000,
                CurrentPrice = 100,
                DayHighPrice = 110,
                DayLowPrice = 90,
                OpeningPriceToday = 105,
                SellPrice1 = 115,
                SellPrice2 = 114,
                SellPrice3 = 113,
                SellPrice4 = 112,
                SellPrice5 = 111,
                SellStrand1 = 1000,
                SellStrand2 = 2000,
                SellStrand3 = 3000,
                SellStrand4 = 4000,
                SellStrand5 = 5000,
            };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < count; i++)
            {
                _ = new SourceModel()
                {
                    ID = source.ID,
                    Name = source.Name,
                    Amount = source.Amount,
                    AuctionPrice = source.AuctionPrice,
                    BiddingPrice = source.BiddingPrice,
                    BuyPrice1 = source.BuyPrice1,
                    BuyPrice2 = source.BuyPrice2,
                    BuyPrice3 = source.BuyPrice3,
                    BuyPrice4 = source.BuyPrice4,
                    BuyPrice5 = source.BuyPrice5,
                    BuyStrand1 = source.BuyStrand1,
                    BuyStrand2 = source.BuyStrand2,
                    BuyStrand3 = source.BuyStrand3,
                    BuyStrand4 = source.BuyStrand4,
                    BuyStrand5 = source.BuyStrand5,
                    ClosingPriceYesterday = source.ClosingPriceYesterday,
                    Count = source.Count,
                    CurrentPrice = source.CurrentPrice,
                    DayHighPrice = source.DayHighPrice,
                    DayLowPrice = source.DayLowPrice,
                    OpeningPriceToday = source.OpeningPriceToday,
                    SellPrice1 = source.SellPrice1,
                    SellPrice2 = source.SellPrice2,
                    SellPrice3 = source.SellPrice3,
                    SellPrice4 = source.SellPrice4,
                    SellPrice5 = source.SellPrice5,
                    SellStrand1 = source.SellStrand1,
                    SellStrand2 = source.SellStrand2,
                    SellStrand3 = source.SellStrand3,
                    SellStrand4 = source.SellStrand4,
                    SellStrand5 = source.SellStrand5,
                };
            }
            stopwatch.Stop();
            Helper.WriteLine($"手写复制代码-克隆耗时：{stopwatch.ElapsedMilliseconds.ToString("N0")} ms");

            stopwatch.Restart();
            Expression<Func<SourceModel, SourceModel>> expression = (q => new SourceModel()
            {
                ID = q.ID,
                Name = q.Name,
                Amount = q.Amount,
                AuctionPrice = q.AuctionPrice,
                BiddingPrice = q.BiddingPrice,
                BuyPrice1 = q.BuyPrice1,
                BuyPrice2 = q.BuyPrice2,
                BuyPrice3 = q.BuyPrice3,
                BuyPrice4 = q.BuyPrice4,
                BuyPrice5 = q.BuyPrice5,
                BuyStrand1 = q.BuyStrand1,
                BuyStrand2 = q.BuyStrand2,
                BuyStrand3 = q.BuyStrand3,
                BuyStrand4 = q.BuyStrand4,
                BuyStrand5 = q.BuyStrand5,
                ClosingPriceYesterday = q.ClosingPriceYesterday,
                Count = q.Count,
                CurrentPrice = q.CurrentPrice,
                DayHighPrice = q.DayHighPrice,
                DayLowPrice = q.DayLowPrice,
                OpeningPriceToday = q.OpeningPriceToday,
                SellPrice1 = q.SellPrice1,
                SellPrice2 = q.SellPrice2,
                SellPrice3 = q.SellPrice3,
                SellPrice4 = q.SellPrice4,
                SellPrice5 = q.SellPrice5,
                SellStrand1 = q.SellStrand1,
                SellStrand2 = q.SellStrand2,
                SellStrand3 = q.SellStrand3,
                SellStrand4 = q.SellStrand4,
                SellStrand5 = q.SellStrand5,
            });
            var mapFunc = expression.Compile();
            for (int i = 0; i < count; i++)
            {
                _ = mapFunc(source);
            }
            stopwatch.Stop();
            Helper.WriteLine($"手写表达式树-克隆耗时：{stopwatch.ElapsedMilliseconds.ToString("N0")} ms");

            stopwatch.Restart();
            for (int i = 0; i < count; i++)
            {
                _ = ExpressionCloneExtension<SourceModel, SourceModel>.Clone(source);
            }
            stopwatch.Stop();
            Helper.WriteLine($"泛型表达式树-克隆耗时：{stopwatch.ElapsedMilliseconds.ToString("N0")} ms");

            stopwatch.Restart();
            for (int i = 0; i < count; i++)
            {
                var target = ExpressionCloneExtension<SourceModel, TargetModel>.Clone(source);
            }
            stopwatch.Stop();
            Helper.WriteLine($"泛型表达式树-克隆耗时：{stopwatch.ElapsedMilliseconds.ToString("N0")} ms");

            stopwatch.Restart();
            for (int i = 0; i < count; i++)
            {
                _ = ReflectionCloneExtension.Clone(source);
            }
            stopwatch.Stop();
            Helper.WriteLine($"自动反射属性-克隆耗时：{stopwatch.ElapsedMilliseconds.ToString("N0")} ms");

            Console.Read();
        }
    }
}
