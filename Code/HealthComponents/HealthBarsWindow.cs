using Aid.UI;

namespace Aid.HealthComponents
{
    public class HealthBarsWindow : Window
    {
        protected override void OnShown()
        {
            HealthBarPresentersRegistry.Show();
        }

        protected override void OnHidden()
        {
            HealthBarPresentersRegistry.Hide();
        }
    }
}