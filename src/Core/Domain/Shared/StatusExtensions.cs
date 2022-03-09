using Actions.Core.Domain.Shared.Dtos;
using Shared.Core.Domain.Impl.Attributes;
using Shared.CrossCutting.Tools;
using System;

namespace Actions.Core.Domain.Shared
{
    public static class StatusExtensions
    {        
        public static StatusDTO Status(this System.Enum status)
        {
            return new StatusDTO()
            {
                Id = Convert.ToInt16(status),
                Text = status.Description(),
                Color = status.GetAttribute<ColorAttribute>().Value,
            };
        }
    }
}
