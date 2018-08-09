<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdminMain.aspx.cs" Inherits="Restaurante.MainAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:Panel runat="server">
            <asp:Label ID="lblMain" runat="server" Text="Bienvenido al Menu Principal"></asp:Label>
            <asp:TextBox ID="txtPrincipal" runat="server" placeholder ="Ingrese Texto"></asp:TextBox>
            <asp:Button ID="btnPrincipal" runat="server" Text="Ingresar" />
        </asp:Panel>
</asp:Content>
