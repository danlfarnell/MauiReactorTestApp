using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;
using MauiReactorTestApp.Components;

namespace MauiReactorTestApp.Pages;

class MainPageState
{
    public string Email { get; set; }
    public bool EmailValid { get; set; }
    public string Password { get; set; }
    public bool PasswordValid { get; set; }
}

class MainPage : Component<MainPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage
        {
            new VStack(spacing: 20)
            {
                new Label("Create Your Account").Class("LargeLabel").HCenter(),
                new EmailEntry()
                    .OnTextChanged((text, validated) =>
                        SetState(s =>
                            {
                                if (!validated)
                                {
                                    s.EmailValid = false;
                                    return;
                                }
               
                                s.Email = text;
                                s.EmailValid = true;
                            }
                        )),
                new PasswordEntry()
                    .OnTextChanged((text, validated) =>
                    {
                        SetState(s =>
                        {
                            if (!validated)
                            {
                                s.PasswordValid = false;
                                return;
                            }
                            
                            s.Password = text;
                            s.PasswordValid = true;
                        });
                    }),
                new SignUpButton()
                    .PageToNavigateTo("SignUpPage")
                    .IsEnabled(State.EmailValid && State.PasswordValid),
             
            }.Padding(10).Margin(0, 80, 0, 0)
        }.Class("Page");
        
    }
}