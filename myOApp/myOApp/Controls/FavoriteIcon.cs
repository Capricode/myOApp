using Lottie.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace myOApp.Controls
{
    public class FavoriteIcon : AnimationView
    {
        public FavoriteIcon()
        {
            Animation = "favoriteAnimation.json";
            OnClick += Icon_OnClick;
        }

        public static readonly BindableProperty IsFavoritedProperty = BindableProperty.Create(nameof(IsFavorited), typeof(bool), typeof(FavoriteIcon), defaultBindingMode: BindingMode.TwoWay, propertyChanged: IsFavoritedToggled);

        public bool IsFavorited
        {
            get { return (bool)GetValue(IsFavoritedProperty); }
            set { SetValue(IsFavoritedProperty, value); }
        }

        static void IsFavoritedToggled(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is FavoriteIcon fav)) return;

            if ((bool)newValue)
            {
                fav.Play();
            }
            else
            {
                fav.Reset(); // https://intelliabb.com/2019/02/17/stunning-animations-in-xamarin-forms-with-lottie/
            }
        }

        void Reset()
        {
            Animation = null;
            // extract to const
            Animation = "favoriteAnimation.json";
        }

        private void Icon_OnClick(object sender, EventArgs e)
        {
            IsFavorited = !IsFavorited;
        }
    }
}
