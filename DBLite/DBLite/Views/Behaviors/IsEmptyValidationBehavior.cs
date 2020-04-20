using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DBLite.Views.Behaviors
{
    class IsEmptyValidationBehavior : Behavior<Entry>
    {
		protected override void OnAttachedTo(Entry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(entry);
		}

		protected override void OnDetachingFrom(Entry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			base.OnDetachingFrom(entry);
		}

		void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			bool isValid = args.NewTextValue.Length > 0;
			((Entry)sender).PlaceholderColor = ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
		}
	}
}
