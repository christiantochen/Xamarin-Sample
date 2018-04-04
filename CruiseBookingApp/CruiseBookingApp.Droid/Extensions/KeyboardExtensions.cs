using Android.OS;
using Android.Views.InputMethods;

namespace Android.Content
{
    public static class KeyboardExtensions
    {
        public static void HideKeyboard(this Context context, IBinder windowToken)
        {
            var manager = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
            manager.HideSoftInputFromWindow(windowToken, 0);
        }
    }
}
