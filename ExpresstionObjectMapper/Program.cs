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
            Helper.WriteLine($"手写复制代码-克隆耗时：{stopwatch.Elapsed.ToString()}");

            stopwatch.Restart();
            for (int i = 0; i < count; i++)
            {
                _ = ReflectionCloneExtension.Clone(source);
            }
            stopwatch.Stop();
            Helper.WriteLine($"自动反射属性-克隆耗时：{stopwatch.Elapsed.ToString()}");

            stopwatch.Restart();
            Expression<Func<SourceModel, SourceModel>> expression = (s => new SourceModel() { ID = s.ID, Name = s.Name });
            var mapFunc = expression.Compile();
            for (int i = 0; i < count; i++)
            {
                _ = mapFunc(source);
            }
            stopwatch.Stop();
            Helper.WriteLine($"手写表达式树-克隆耗时：{stopwatch.Elapsed.ToString()}");

            stopwatch.Restart();
            for (int i = 0; i < count; i++)
            {
                _ = ExpressionCloneExtension<SourceModel, SourceModel>.Clone(source);
            }
            stopwatch.Stop();
            Helper.WriteLine($"泛型表达式树-克隆耗时：{stopwatch.Elapsed.ToString()}");

            stopwatch.Restart();
            for (int i = 0; i < count; i++)
            {
                var target = ExpressionCloneExtension<SourceModel, TargetModel>.Clone(source);
            }
            stopwatch.Stop();
            Helper.WriteLine($"泛型表达式树-克隆耗时：{stopwatch.Elapsed.ToString()}");

            Console.Read();
        }
    }
}
