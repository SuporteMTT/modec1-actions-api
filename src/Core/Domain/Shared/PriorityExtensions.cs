using Actions.Core.Domain.Shared.Dtos;
using Shared.Core.Domain.Impl.Attributes;
using Shared.CrossCutting.Tools;
using System;

namespace Actions.Core.Domain.Shared
{
    public static class PriorityExtensions
    {
        public static PriorityDTO Priority(this System.Enum priority)
        {
            return new PriorityDTO()
            {
                Id = Convert.ToInt16(priority),
                Text = priority.ValorNumerico() != 0 ? priority.Description() : "",
                Color = priority.ValorNumerico() != 0 ? priority.GetAttribute<ColorAttribute>().Value : "",
            };
        }
    }
}
