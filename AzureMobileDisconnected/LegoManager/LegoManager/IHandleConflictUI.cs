using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoManager
{
    public interface IHandleConflictUI
    {
        Task<ConflictResolutionDecision> HandleConflictAsync(string message);
    }

    public enum ConflictResolutionDecision
    {
        Server,
        Local
    }
}
