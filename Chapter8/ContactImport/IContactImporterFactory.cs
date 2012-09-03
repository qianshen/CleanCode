using System;
using System.Collections.Generic;
using System.Text;

namespace EFSchools.Englishtown.Community.Common.ContactImport
{
    internal interface IContactImporterFactory
    {
        IContactImporter CreateContactImporter(string username, string password, string type);
    }
}
