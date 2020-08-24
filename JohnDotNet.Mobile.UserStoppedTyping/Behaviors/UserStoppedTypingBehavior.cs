﻿using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JohnDotNet.Mobile.UserStoppedTyping.Behaviors
{
    public class UserStoppedTypingBehavior : BehaviorBase<Entry>
    {
        private CancellationTokenSource _tokenSource;

        #region Bindable Properties

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(UserStoppedTypingBehavior));
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty StoppedTypingThresholdProperty = BindableProperty.Create(nameof(StoppedTypingThreshold), typeof(int), typeof(UserStoppedTypingBehavior), defaultValue: 1000);
        public int StoppedTypingThreshold
        {
            get => (int)GetValue(StoppedTypingThresholdProperty);
            set => SetValue(StoppedTypingThresholdProperty, value);
        }

        public static readonly BindableProperty AutoDismissKeyboardProperty = BindableProperty.Create(nameof(AutoDismissKeyboard), typeof(bool), typeof(UserStoppedTypingBehavior), defaultValue: false);
        public bool AutoDismissKeyboard
        {
            get => (bool)GetValue(AutoDismissKeyboardProperty);
            set => SetValue(AutoDismissKeyboardProperty, value);
        }

        #endregion Bindable Properties

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            
            bindable.TextChanged += Entry_TextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.TextChanged -= Entry_TextChanged;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }
            _tokenSource = new CancellationTokenSource();

            PerformTextChanged(sender as Entry, e.NewTextValue, _tokenSource.Token);
        }

        private async Task PerformTextChanged(Entry entry, string newTextValue, CancellationToken token)
        {
            await Task.Delay(StoppedTypingThreshold);

            if (token.IsCancellationRequested) return;

            if (Command != null && Command.CanExecute(newTextValue))
            {
                Command.Execute(newTextValue);

                if (AutoDismissKeyboard)
                {
                    entry.Unfocus();
                }
            }
        }
    }
}