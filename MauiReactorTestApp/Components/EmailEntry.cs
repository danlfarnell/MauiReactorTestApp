using System;
using CommunityToolkit.Maui.Behaviors;
using MauiReactor;

namespace MauiReactorTestApp.Components;

internal class EmailEntry : Component
{
    private TextValidationBehavior _emailValidationBehavior;
    private bool _isThinIcon;
    private string _text;
    private Action<string, bool> _onTextChanged;

    public EmailEntry UseThinIcon(bool useThinIcon)
    {
        _isThinIcon = useThinIcon;
        return this;
    }

    public EmailEntry Text(string text)
    {
        _text = text;
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

        _emailValidationBehavior = new EmailValidationBehavior
        {
            InvalidStyle = invalidStyle,
            ValidStyle = validStyle,
            Flags = ValidationFlags.ValidateOnValueChanged | ValidationFlags.ValidateOnAttaching,
        };


        base.OnMounted();
    }

    public EmailEntry OnTextChanged(Action<string, bool> action)
    {
        _onTextChanged = action;
        return this;
    }

    public override VisualNode Render()
    {
        return _isThinIcon ? RenderThinIcon() : RenderRegularIcon();
    }

    private VisualNode RenderThinIcon()
    {
        return new Border
        {
            new Grid()
            {
                new Entry(ent => ent.Behaviors.Add(_emailValidationBehavior))
                    .Placeholder("Email").Class("NormalEntry")
                    .Text(_text)
                    .HFill()
                    .GridColumn(0)
                    .IsTextPredictionEnabled(false)
                    .OnTextChanged(OnEmailTextChanged),

                new Image()
                    .Source("message_icon_thin")
                    .GridColumn(1)
                    .HEnd(),
            }.Columns("*,Auto")
        }.Class("ControlBorder");
    }

    private void OnEmailTextChanged(object sender, MauiControls.TextChangedEventArgs args)
    {
        if (_emailValidationBehavior.IsNotValid)
        {
            _onTextChanged?.Invoke(args.NewTextValue, false);
            return;
        }

        _onTextChanged?.Invoke(args.NewTextValue, true);
    }


    private VisualNode RenderRegularIcon()
    {
        return new Border
        {
            new HStack(spacing: 5)
            {
                {
                    new Image().Source("email_icon"),
                    new Entry(ent => ent.Behaviors.Add(_emailValidationBehavior))
                        .Placeholder("Email").Class("NormalEntry")
                        .WidthRequest(300)
                        .Text(_text)
                        .IsTextPredictionEnabled(false)
                        .OnTextChanged(OnEmailTextChanged)
                }
            }
        }.Class("ControlBorder");
    }

}