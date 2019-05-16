using System;

namespace ExpresstionObjectMapper.Models
{
    /// <summary>
    /// 目标模型
    /// </summary>
    public class TargetModel : IModel
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public override string ToString() => $"{this.ID.ToString()} - {this.Name}";
    }
}
