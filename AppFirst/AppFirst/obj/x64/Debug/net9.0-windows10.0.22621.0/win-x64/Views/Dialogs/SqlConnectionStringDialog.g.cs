﻿#pragma checksum "U:\_Repozitory\C#\_GitHub1\AppFirst\AppFirst\Views\Dialogs\SqlConnectionStringDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F97B6BAF844C7AFB4D1A64DA90D82384481E4C0F8F46192C493CE6C680C774DE"
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
    partial class SqlConnectionStringDialog : 
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
            case 2: // Views\Dialogs\SqlConnectionStringDialog.xaml line 219
                {
                    this.txtSqlConnectionString = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 3: // Views\Dialogs\SqlConnectionStringDialog.xaml line 188
                {
                    this.txtAttachDBFilename = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 4: // Views\Dialogs\SqlConnectionStringDialog.xaml line 159
                {
                    this.chkIntegratedSecurity = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CheckBox>(target);
                }
                break;
            case 5: // Views\Dialogs\SqlConnectionStringDialog.xaml line 130
                {
                    this.pwdPassword = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.PasswordBox>(target);
                }
                break;
            case 6: // Views\Dialogs\SqlConnectionStringDialog.xaml line 101
                {
                    this.txtUsername = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 7: // Views\Dialogs\SqlConnectionStringDialog.xaml line 71
                {
                    this.txtDatabase = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 8: // Views\Dialogs\SqlConnectionStringDialog.xaml line 40
                {
                    this.cmbServer = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
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

