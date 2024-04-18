using System.Collections.Generic;

namespace Aid.HealthComponents
{
    public static class HealthBarPresentersRegistry
    {
        private static readonly List<HealthBarPresenter> Presenters = new();


        private static bool _shown;

        public static void Show()
        {
            if (_shown) return;
            _shown = true;
            InjectHealthBars();
        }

        public static void Hide()
        {
            if (!_shown) return;
            _shown = false;
            Dispose();
        }

        public static void Register(HealthBarPresenter presenter)
        {
            Presenters.Add(presenter);

            if (_shown)
            {
                Inject(presenter);
            }
        }

        public static void Unregister(HealthBarPresenter presenter)
        {
            Presenters.Remove(presenter);

            if (_shown)
            {
                presenter.Dispose();
            }
        }

        public static void InjectHealthBars()
        {
            foreach (var presenter in Presenters)
            {
                Inject(presenter);
            }
        }

        private static void Inject(HealthBarPresenter presenter)
        {
            var bar = HealthBarsPool.Instance.Get(presenter.barFactory);
            presenter.InjectHealthBar(bar);
        }


        public static void Dispose()
        {
            foreach (var presenter in Presenters)
            {
                presenter.Dispose();
            }
        }
    }
}