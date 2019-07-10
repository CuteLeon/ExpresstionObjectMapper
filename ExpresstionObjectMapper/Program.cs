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
            SourceModel source = new SourceModel() { ID = Guid.NewGuid(), Name = "Source Model Instance" };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < count; i++)
            {
                _ = new SourceModel() { ID = source.ID, Name = source.Name };
            }
            stopwatch.Stop();
            Helper.WriteLine($"手写复制代码-克隆耗时：{stopwatch.ElapsedMilliseconds.ToString("N0")} ms");

            stopwatch.Restart();
            for (int i = 0; i < count; i++)
            {
                _ = ReflectionCloneExtension.Clone(source);
            }
            stopwatch.Stop();
            Helper.WriteLine($"自动反射属性-克隆耗时：{stopwatch.ElapsedMilliseconds.ToString("N0")} ms");

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

            Console.Read();
        }
    }
}
