using System;
using System.Collections.Generic;
using System.Text;

namespace EFSchools.Englishtown.Community.Common.ContactImport
{
    public interface IContactListImportService
    {
        ImportResult GetContacts(string username, string password, string emailDomain, out IList<EmailContact> contacts);
        IList<EmailContact> CreateDefaultEmailContacts(int numberOfContacts);
    }
}
