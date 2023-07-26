using System;
using CommunityToolkit.Maui.Behaviors;
using MauiReactor;

namespace MauiReactorTestApp.Components;

public class PasswordEntry : Component
{
    private TextValidationBehavior _textValidationBehavior;
    private string _text;
    private Action<string, bool> _onTextChanged;


    public PasswordEntry Text(string text)
    {
        _text = text;
        return this;
    }

    public PasswordEntry OnTextChanged(Action<string, bool> action)
    {
        _onTextChanged = action;
        return this;
    }

    protected override void OnMounted()
    {
        var validStyle = new MauiControls.Style(typeof(Entry));
        validStyle.Setters.Add(
            new MauiControls.Setter
            {
                Property = MauiControls.Entry.TextColorProperty,
                Value = Colors.White
            });

        var invalidStyle = new MauiControls.Style(typeof(Entry));
        invalidStyle.Setters.Add(
            new MauiControls.Setter
            {
                Property = MauiControls.Entry.TextColorProperty,
                Value = Colors.Gray
            });

        _textValidationBehavior = new TextValidationBehavior
        {
            InvalidStyle = invalidStyle,
            ValidStyle = validStyle,
            Flags = ValidationFlags.ValidateOnValueChanged | ValidationFlags.ValidateOnAttaching,
            MinimumLength = 1,
            MaximumLength = 10
        };


        base.OnMounted();
    }

    public override VisualNode Render()
    {
        return new Border
        {
            new HorizontalStackLayout(spacing: 5)
            {
                new Image().Source("password_icon"),
                new Entry(ent => ent.Behaviors.Add(_textValidationBehavior)).Placeholder("Password")
                    .Class("NormalEntry")
                    .Text(_text)
                    .IsPassword(true).WidthRequest(300)
                    .OnTextChanged(OnPasswordTextChanged)
            }
        }.Class("ControlBorder");
    }

    private void OnPasswordTextChanged(object arg1, MauiControls.TextChangedEventArgs arg2)
    {
        if (_textValidationBehavior.IsNotValid)
        {
            _onTextChanged?.Invoke(arg2.NewTextValue, false);
            return;
        }

        _onTextChanged?.Invoke(arg2.NewTextValue, true);
    }
}