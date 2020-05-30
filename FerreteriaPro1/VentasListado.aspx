<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VentasListado.aspx.cs" Inherits="FerreteriaPro1.VentasListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Listado de ventas</h3>
    <div class="row">
        <div class="col-lg-12">
            <input type="button" class="btn btn-primary" value="Agregar" onclick="location.href = 'Venta.aspx?op=1';" />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <asp:GridView ID="dgvListado" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-striped table-bordered table-hover dataTable no-footer" role="grid">
                    <Columns>
                        <asp:BoundField AccessibleHeaderText="id_venta" DataField="id_venta" HeaderText="Códgio">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info " />
                        </asp:BoundField>
                        <asp:HyperLinkField AccessibleHeaderText="fecha_venta" DataTextField="fecha_venta" HeaderText="Fecha de venta" DataNavigateUrlFields="id_venta" DataNavigateUrlFormatString="Venta.aspx?idv={0}&op=4" DataTextFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle CssClass="info" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField AccessibleHeaderText="nombre_cliente" DataField="nombre_cliente" HeaderText="Nombre cliente">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="nombre_ferreteria" DataField="nombre_ferreteria" HeaderText="Nombre ferreteria">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="monto_total_venta" DataField="monto_total_venta" HeaderText="Monto total venta">
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
