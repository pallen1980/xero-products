using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroProducts.BL.CustomExceptions
{
    public class SuperAdminDeletionAttemptException : Exception
    {
        public SuperAdminDeletionAttemptException() : base() { }
        public SuperAdminDeletionAttemptException(string message): base(message) { }
    }
}
