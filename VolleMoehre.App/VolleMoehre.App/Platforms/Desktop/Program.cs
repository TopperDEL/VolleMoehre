using Microsoft.UI.Xaml;

namespace VolleMoehre.App
{
    public class Program
    {
        [System.STAThreadAttribute]
        static void Main(string[] args)
        {
            Microsoft.UI.Xaml.Application.Start((p) =>
            {
                var context = new Microsoft.UI.Dispatching.DispatcherQueueSynchronizationContext(
                    Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread());
                System.Threading.SynchronizationContext.SetSynchronizationContext(context);
                _ = new App();
            });
        }
    }
}
