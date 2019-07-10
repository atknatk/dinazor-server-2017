
using Dinazor.Core.Model;

namespace Dinazor.Core.Observer
{
    public interface ILoginNotify
    {
        void Notify(TokenUser tokenUser);
    }
}
