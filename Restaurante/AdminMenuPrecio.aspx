﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="AdminMenuPrecio.aspx.cs" Inherits="Restaurante.AdminMenuPrecio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelMenus" runat="server">
        <asp:GridView ID="GrillaMenus" runat="server" AutoGenerateColumns="false" DataKeyNames="Id">
            <Columns> 
                <asp:BoundField DataField ="Descripcion" HeaderText ="Descripcion"/>
                <asp:BoundField DataField ="PrecioVenta" HeaderText ="Precio"/>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
