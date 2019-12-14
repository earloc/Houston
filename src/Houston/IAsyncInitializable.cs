using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Houston
{
    public interface IAsyncInitializable
    {
        Task InitializeAsync();
    }
}
