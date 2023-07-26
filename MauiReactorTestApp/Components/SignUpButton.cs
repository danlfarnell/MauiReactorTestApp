using MauiReactor;

namespace MauiReactorTestApp.Components;

internal class SignUpButton : Component
{
    private string _nextPage;
    private bool _isEnabled;

    protected override void OnMounted()
    {
        base.OnMounted();
    }

    public SignUpButton PageToNavigateTo(string nextPage)
    {
        _nextPage = nextPage;
        return this;
    }

    public SignUpButton IsEnabled(bool isEnabled)
    {
         _isEnabled = isEnabled;
        return this;
    }


    public override VisualNode Render()
    {
        return new Button()
            .Text("Sign up")
            .Class("NextButton")
            .WidthRequest(300)
            .HCenter()
            .IsEnabled(_isEnabled)
            .OnClicked(OnSignUpButtonClicked);
    }

    private async void OnSignUpButtonClicked()
    {
      //GO TO next page....
    }
}