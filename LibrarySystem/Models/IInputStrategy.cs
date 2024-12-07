using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Console_App.LibrarySystem.Models
{
    public interface IInputStrategy
    {
        void CollectInput(IDGenerator iDGenerator);
    }
}
