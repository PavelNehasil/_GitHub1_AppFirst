﻿#pragma checksum "U:\_Repozitory\C#\_GitHub1\AppFirst\AppFirst\Views\Dialogs\LoginDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BCF354E3E937CD9CADE35CDF299B8826BF694F50D9501B81417B122D163A3F9A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppFirst.Views.Dialogs
{
    partial class LoginDialog : 
        global::Microsoft.UI.Xaml.Controls.ContentDialog, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Views\Dialogs\LoginDialog.xaml line 1
                {
                    global::Microsoft.UI.Xaml.Controls.ContentDialog element1 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ContentDialog>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ContentDialog)element1).CloseButtonClick += this.ContentDialog_CloseButtonClick;
                    ((global::Microsoft.UI.Xaml.Controls.ContentDialog)element1).PrimaryButtonClick += this.ContentDialog_PrimaryButtonClick;
                }
                break;
            case 2: // Views\Dialogs\LoginDialog.xaml line 20
                {
                    this.userNameTextBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.userNameTextBox).TextChanged += this.UserNameTextBox_TextChanged;
                }
                break;
            case 3: // Views\Dialogs\LoginDialog.xaml line 25
                {
                    this.passwordTextBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.PasswordBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.PasswordBox)this.passwordTextBox).PasswordChanged += this.PasswordTextBox_PasswordChanged;
                }
                break;
            case 4: // Views\Dialogs\LoginDialog.xaml line 31
                {
                    this.errorInfoBar = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.InfoBar>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }


        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
