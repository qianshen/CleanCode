using System;
using System.Collections.Generic;
using System.Text;

namespace EFSchools.Englishtown.Community.Common.ContactImport
{
    internal interface IContactImporter : IDisposable
    {
        string[] emailArray { get; }
        bool logged_in { get; }
        string[] nameArray { get; }
        void getcontacts();
        void login();
    }
}
