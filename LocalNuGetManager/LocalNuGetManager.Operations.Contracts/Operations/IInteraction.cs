using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNuGetManager.Operations.Contracts.Operations
{
    public interface IInteraction<TDto> where TDto : class
    {
        public void Interact(TDto dto);
    }
}
