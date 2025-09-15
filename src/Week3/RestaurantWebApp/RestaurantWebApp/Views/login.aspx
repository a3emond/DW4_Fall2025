<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SimpleWebApp.Views.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <h1>Login</h1>

        <!-- Login fields -->
        <div class="form-fields">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="input" TextMode="Email" placeholder="Email" />
            <ul id="email-feedback" class="feedback"></ul>

            <asp:TextBox ID="txtPassword" runat="server" CssClass="input" TextMode="Password" placeholder="Password" />
            <ul id="password-feedback" class="feedback"></ul>

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" />

            <asp:Label ID="lblError" runat="server" CssClass="error" />
        </div>

        <p style="text-align:center; margin-top:15px;">
            Not registered? <a href="/register">Register</a>
        </p>
    </div>
</asp:Content>
