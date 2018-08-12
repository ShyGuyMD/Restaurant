<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="RealizarReserva.aspx.cs" Inherits="Restaurante.RealizarReserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelInput" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Para ingresar su reserva, complete los campos requeridos:"></asp:Label>
        <br />
        <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblPersonas" runat="server" Text="Cantidad de Personas:"></asp:Label>
        <asp:TextBox ID="txtPersonas" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
        <asp:Calendar ID="calFecha" runat="server"></asp:Calendar>
        <br />
        <asp:Label ID="lblHora" runat="server" Text="Hora:"></asp:Label>
        <asp:TextBox ID="txtHoras" runat="server"></asp:TextBox>
        <asp:Label ID="lblSeparador" runat="server" Text=":"></asp:Label>
        <asp:TextBox ID="txtMinutos" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblMesa" runat="server" Text="Mesa:"></asp:Label>
        <asp:DropDownList ID="lstMesa" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="lblMenu" runat="server" Text="Menúes Solicitados:"></asp:Label>
        <asp:DropDownList ID="lstMenu" runat="server"></asp:DropDownList>
        <asp:Button ID="BtnAgregarMenu" runat="server" Text="Agregar" OnClick="BtnAgregarMenu_Click" />
        <br />
        <asp:GridView ID="grillaMenus" runat="server" AutoGenerateColumns="false" OnRowCommand="grillaMenus_RowCommand">
            <Columns>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" />
                <asp:ButtonField ButtonType="Button" CommandName="eliminar" />
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnReservar" runat="server" Text="Reservar" OnClick="btnReservar_Click" />
    </asp:Panel>
</asp:Content>
