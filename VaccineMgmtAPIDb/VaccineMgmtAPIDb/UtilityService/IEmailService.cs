using System;
using VaccineMgmtAPIDb.Models;

namespace VaccineMgmtAPIDb.UtilityService
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}
