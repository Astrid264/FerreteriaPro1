<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompraListado.aspx.cs" Inherits="FerreteriaPro1.CompraListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Listado de compras</h3>
    <div class="row">
        <div class="col-lg-12">
            <input type="button" class="btn btn-primary" value="Agregar" onclick="location.href = 'Compra.aspx?op=1';" />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <asp:GridView ID="dgvListado" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-striped table-bordered table-hover dataTable no-footer" role="grid">
                    <Columns>
                        <asp:BoundField AccessibleHeaderText="id_compra" DataField="id_compra" HeaderText="Código">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info " />
                        </asp:BoundField>
                        <asp:HyperLinkField AccessibleHeaderText="fecha_compra" DataTextField="fecha_compra" HeaderText="Fecha de compra" DataNavigateUrlFields="id_compra" DataNavigateUrlFormatString="Compra.aspx?idc={0}&op=4" DataTextFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle CssClass="info" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField AccessibleHeaderText="nombre_proveedor" DataField="nombre_proveedor" HeaderText="Nombre proveedor">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="monto_total_compra" DataField="monto_total_compra" HeaderText="Monto total compra">
                            <ItemStyle HorizontalAlign="Right" CssClass="" />
                            <HeaderStyle CssClass="text-right info" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
            </div>
        </div>
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
