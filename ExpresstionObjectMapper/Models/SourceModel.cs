using System;

namespace ExpresstionObjectMapper.Models
{
    /// <summary>
    /// 源模型
    /// </summary>
    public class SourceModel : IModel
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public override string ToString() => $"{this.ID.ToString()} - {this.Name}";
    }
}
