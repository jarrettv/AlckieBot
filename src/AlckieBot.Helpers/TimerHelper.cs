using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Helpers
{
    public static class TimerHelper
    {
        public static async void ExecuteDelayedActionAsync(Action action, TimeSpan time)
        {
            await Task.Delay(time);
            action.Invoke();
        }
    }
}
