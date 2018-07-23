using System;
using System.Collections.Generic;
using System.Text;

namespace Office.App.Core.Contracts
{
    public interface ICommandIntrepreter
    {
        string Read(string[] input);
    }
}
