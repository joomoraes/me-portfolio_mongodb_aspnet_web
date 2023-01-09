using Web.Application.Domain.Enums;

namespace Web.Application.Domain.Entities
{
    public class Login
    {
        public Login(
            string _UserId,
            string _UserToken,
            DateTime _openSession,
            DateTime _expirationSession,
            EStatusLogin _status
            )
        {
            _UserId = UserId;
            _UserToken = UserToken;
            _openSession = openSession;
            _expirationSession = expirationSession;
            _status = status;
        }
        public string UserId { get; set; }
        public string UserToken { get; set; }
        public DateTime openSession { get; set; }
        public DateTime expirationSession { get; set; }
        public EStatusLogin status { get; set; }

    }
}
