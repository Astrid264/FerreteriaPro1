<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Abastecimiento.aspx.cs" Inherits="FerreteriaPro1.Abastecimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Abastecimiento de artículos a ferreterías</h3>
    <div class="row">
        <div class="col-lg-3"></div>
        <div class="col-lg-6">
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:DropDownList runat="server" ID="cmbArticulos" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-3"></div>
    </div>
    <div class="row">
        <div class="col-lg-3"></div>
        <div class="col-lg-6">
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-phone"></i></span>
                <asp:TextBox runat="server" ID="txtCantidad" CssClass="form-control" placeholder="Cantidad" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-3"></div>
    </div>
    <div class="row">
        <div class="col-lg-3"></div>
        <div class="col-lg-6">
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:DropDownList runat="server" ID="cmbIdFerreteria" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-3"></div>
    </div>
    <div class="row">
        <div class="col-lg-3"></div>
        <div class="col-lg-6">
            <asp:Button runat="server" ID="btnGuardar" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click"></asp:Button>
        </div>
        <div class="col-lg-3"></div>
    </div>
    <div class="row">

        <% 
            if (_MensajeError != "")
            {
        %>
        <div class="alert alert-danger">
            <%=_MensajeError %>
        </div>
        <%
            }
            if (_MensajeSatisfactorio != "")
            {
        %>
        <div class="alert alert-success">
            <%=_MensajeSatisfactorio %>
        </div>
        <%
            }

        %>
    </div>
</asp:Content>
