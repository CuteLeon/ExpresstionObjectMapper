using System;

namespace ExpresstionObjectMapper.Models
{
    public interface IModel
    {
        Guid ID { get; set; }

        string Name { get; set; }
    }
}
