using QuickActions.Common.Data;

namespace QuickActions.Common.Interfaces
{
    public interface IIdentity<T>
    {
        public Task Logout();
        public Task<Session<T>> Authenticate();
    }
}
