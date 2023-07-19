using Microsoft.AspNetCore.Http;
using QuickActions.Common.Data;

namespace QuickActions.Api.Identity.Services
{
    public class SessionsService<T>
    {
        protected Dictionary<string, Session<T>> sessions = new();

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string keyName;
        private readonly long sessionLifeTime;

        public SessionsService(IHttpContextAccessor httpContextAccessor, string keyName, long sessionLifeTime)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.keyName = keyName;
            this.sessionLifeTime = sessionLifeTime;
        }

        public string CreateSession(Session<T> session)
        {
            var key = Guid.NewGuid().ToString();

            lock (sessions)
            {
                sessions.Add(key, session);
            }
            return key;
        }

        public Session<T> ReadSession(string key)
        {
            DeleteExpiredSessions();
            if (string.IsNullOrWhiteSpace(key)) return default;
            
            lock (sessions)
            {
                return sessions.GetValueOrDefault(key);
            }
        }

        public Session<T> ReadSession(HttpContext context = null)
        {
            context ??= httpContextAccessor.HttpContext;
            var key = context.Request.Cookies[keyName];
            return ReadSession(key);
        }

        public void DeleteSession(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            lock (sessions)
            {
                sessions.Remove(key);
            }
        }

        public void DeleteSession(HttpContext context = null)
        {
            context ??= httpContextAccessor.HttpContext;
            var key = context.Request.Cookies[keyName];
            DeleteSession(key);
        }

        private void DeleteExpiredSessions()
        {
            lock (sessions)
            {
                var sessionsKeysToDelete = sessions.Where(s => s.Value.CreatedAt <= DateTime.UtcNow.AddMinutes(sessionLifeTime)).Select(s => s.Key).ToList();
                sessionsKeysToDelete.ForEach(str => sessions.Remove(str));
            }
        }
    }
}