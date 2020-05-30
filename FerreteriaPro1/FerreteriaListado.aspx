<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FerreteriaListado.aspx.cs" Inherits="FerreteriaPro1.FerreteriaListado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Listado de ferreterías</h3>
    <div class="row">        
        <div class="col-lg-12">
            <input type="button" class="btn btn-primary" value="Agregar" onclick="location.href = 'Ferreteria.aspx';" />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <asp:GridView ID="dgvListado" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-striped table-bordered table-hover dataTable no-footer" role="grid">
                    <Columns>
                        <asp:BoundField AccessibleHeaderText="id_ferreteria" DataField="id_ferreteria" HeaderText="id_ferreteria">
                            <ItemStyle HorizontalAlign="Center" CssClass="hidden" />
                            <HeaderStyle CssClass="text-center info hidden" />
                        </asp:BoundField>
                        <asp:HyperLinkField AccessibleHeaderText="nombre" DataTextField="nombre" HeaderText="Nombre de la ferreteria" DataNavigateUrlFields="id_ferreteria " DataNavigateUrlFormatString="Ferreteria.aspx?idf={0}&op=4">
                            <HeaderStyle CssClass="info" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField AccessibleHeaderText="telefono" DataField="telefono" HeaderText="Telefono">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="tipo_ferreteria" DataField="tipo_ferreteria" HeaderText="tipo ferreteria">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
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
