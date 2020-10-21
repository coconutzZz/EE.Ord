using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EE.Ord.Main.App.Client.Infrastructure
{
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;

        public static Func<DateTime> UtcNow = () => DateTime.UtcNow;
    }
}
