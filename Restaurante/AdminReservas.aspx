<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdminReservas.aspx.cs" Inherits="Restaurante.AdminReservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="PanelInput" runat="server">
        <asp:Calendar ID="calFecha" runat="server"></asp:Calendar>
        <asp:Button ID="btnFech" runata="server" Text="Button" />
    </asp:Panel>

    <asp:Panel ID="PanelGrilla" runat="server">
        <asp:GridView ID="GrillaReservas" runat="server" AutoGenerateColumns="false" DataKeyNames="Id">
            <Columns> 
                <asp:BoundField DataField ="Nombre" HeaderText ="Nombre"/>
                <asp:BoundField DataField ="Fecha" HeaderText ="FechaReserva"/>
                <asp:BoundField DataField ="Cantidad de Personas" HeaderText ="CantPersonas"/>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
