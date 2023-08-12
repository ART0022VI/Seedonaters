using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;
namespace Seedonaters
{
    public class Config : IConfig
    {
        public List<string> Coins = new List<string>
            {
            "76561198887558606",
            };

        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;
    }
}
